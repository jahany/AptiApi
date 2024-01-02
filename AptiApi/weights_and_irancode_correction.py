from numpy import array, percentile, sort
import pyodbc
from tqdm import tqdm
from collections import Counter
import re


def convergence_indicator(data_list, variation_index):
    if len(data_list) > 2:
        raw_data_mean = sum(data_list) / len(data_list)
        upper = max(data_list) - raw_data_mean
        lower = raw_data_mean - min(data_list)
        total = max(lower, upper)
        if (total / raw_data_mean) <= variation_index:
            return True
        else:
            return False
    else:
        return True


def remove_farthest(data_list):
    d = 0
    idx = 0
    raw_data_mean = sum(data_list) / len(data_list) if len(data_list) != 0 else 0
    for i in range(len(data_list)):
        if abs(data_list[i] - raw_data_mean) > d:
            d = abs(data_list[i] - raw_data_mean)
            idx = i
    if len(data_list) > 2 and d > 0:
        del data_list[idx]


def outlier_remover(data_list, outlier_margin=0.1):
    while not convergence_indicator(data_list, outlier_margin):
        remove_farthest(data_list)
    return sort(array(data_list))


def outlier_remover_2(data_list, outlier_margin=1.5):
    if len(data_list) > 5:
        a = array(data_list)
        upper_quartile = percentile(a, 75)
        lower_quartile = percentile(a, 25)
        IQR = max(((upper_quartile - lower_quartile) * outlier_margin), 8)
        quartile_set = (lower_quartile - IQR, upper_quartile + IQR)
        result_list = []
        for raw_number in a.tolist():
            if quartile_set[0] <= raw_number <= quartile_set[1]:
                result_list.append(raw_number)
        return sort(array(result_list))
    else:
        return sort(array(data_list))


def extract_weight(raw_data, idx):
    weights = []
    for i in range(1, 11):
        for weight in raw_data[idx][i].split("-"):
            if int(float(weight)) > 8:
                weights.append(int(float(weight)))
    return weights


def extract_irancode(raw_data, idx):
    irancodes = []
    for irc in raw_data[idx][11].split("-"):
        if not (irc == "" or irc is None or irc == "0" or len(irc) != 16):
            irancodes.append(irc)
    return irancodes


def merg_iranecode(irancodes: [str]):
    if len(irancodes) > 0:
        irc = Counter(irancodes).most_common(1)[0][0]
        return irc
    else:
        return None


def extract_name(raw_data, idx):
    return raw_data[idx][12]


def estimate_weight(productName: str, quantifiers: [str]) -> int:
    final_weight = 0
    qnt_list = []
    qnf_list = []
    for qnf in quantifiers:
        ptr = ".* (.+)" + qnf + ".*"
        res = re.search(ptr, productName)
        if res is not None:
            quantifier = qnf
            quantity = re.sub("[^0-9\.]", "", res.group(1))
            if quantity != "":
                quantity = float(quantity)
            else:
                quantity = 1

            # normalize numeric:
            if (
                quantifier == "عدد"
                or quantifier == "رول"
                or quantifier == "برگ"
                or quantifier == "قلو"
            ):
                quantity = int(quantity)
                quantifier = "عدد"

            # weight normalization:
            if quantifier == "کیلو" or quantifier == "kg":
                quantifier = "گرم"
                quantity = int(quantity * 1000)
            elif quantifier == "g" or quantifier == "گرم":
                quantifier = "گرم"
                quantity = int(quantity)
            elif quantifier == "مثقال":
                quantifier = "گرم"
                quantity = int(quantity * 4)  # 4.608

            # length normalization:
            elif quantifier == "سانت":
                quantity = int(quantity)
            elif quantifier == "متر":
                quantifier = "سانت"
                quantity = int(quantity * 100)

            # valium normalization:
            elif (
                quantifier == "میلی"
                or quantifier == "سی سی"
                or quantifier == "ml"
                or quantifier == "ML"
                or quantifier == "میل"
            ):
                quantifier = "میلی"
                quantity = int(quantity)
            elif quantifier == "لیتر":
                quantifier = "میلی"
                quantity = int(quantity * 1000)

            qnt_list.append(quantity)
            qnf_list.append(quantifier)

    if len(qnf_list) == 1:
        # if qnf_list[0] == 'عدد':
        #     final_weight = qnt_list[0] * 25
        # elif qnf_list[0] == 'گرم' or qnf_list[0]  == 'میلی' :
        #     final_weight = qnt_list[0]
        if qnf_list[0] == "گرم" or qnf_list[0] == "میلی":
            final_weight = qnt_list[0]
    elif len(qnf_list) > 1:
        for i in range(len(qnf_list)):
            if qnf_list[i] == "عدد":
                for j in range(len(qnf_list)):
                    if qnf_list[j] == "گرم" or qnf_list[j] == "میلی":
                        final_weight = qnt_list[i] * qnt_list[j]
                        break
            elif final_weight == 0 and (qnf_list[i] == "گرم" or qnf_list[i] == "میلی"):
                final_weight = qnt_list[i]
    if final_weight >= 32767:
        with open("overfloow.txt", "w", encoding="utf-8") as f:
            f.write(productName)
        return 0
    return final_weight


def merge_weight(weights):
    clean_weights = outlier_remover(weights)
    server_wights = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
    if len(clean_weights) <= 10:
        for i in range(len(clean_weights)):
            server_wights[i] = int(clean_weights[i])
    else:
        for i in range(len(server_wights) - 1):
            server_wights[i] = int(percentile(clean_weights, (i * 11)))
        server_wights[-1] = int(clean_weights[-1])
    return server_wights


if __name__ == "__main__":

    while 1==1:
        file = open("quantifiers.txt", "r", encoding="utf-8")
        lines = file.readlines()
        qnts = []
        for line in lines:
            qnts.append(line[:-1])
        file.close()

        #conn = pyodbc.connect("Driver={SQL Server};Server=.;Database=APtinet_DataBase;UID=ali;PWD=937PeteryApt;Trusted_Connection=yes;")
        conn = pyodbc.connect("Driver={SQL Server Native Client 11.0};"
                              "Server=localhost,1436;"
                              "Database=APtinet_DataBase;"
                              "UID=sa;"
                              "PWD=Aj253672728282@;")
        
        cursor = conn.cursor()
        print("connection 1")
        cursor.execute("EXEC PrePareWeightsToProccess")
        print("read weights")
        print("start parse data")
        data = []
        rows = cursor.fetchall()
        for row in rows:
            data.append([x for x in row])
            print("1")
        print("end parse data")
        conn.close()
        print("connection 1 closed")
        #conn2 = pyodbc.connect("Driver={SQL Server};Server=.;Database=APtinet_DataBase;UID=ali;PWD=937PeteryApt;Trusted_Connection=yes;")
        conn2 = pyodbc.connect("Driver={SQL Server Native Client 11.0};"
                              "Server=localhost,1436;"
                              "Database=APtinet_DataBase;"
                              "UID=sa;"
                              "PWD=Aj253672728282@;")

        # conn2 = pyodbc.connect(
        #     "Driver={SQL Server};"
        #     "Server=(local);"
        #     "Database=APtinet_DataBase;"
        #     "Trusted_Connection=yes;"
        #     "Integrated Security=True;"
        # )
        print("connection 2")
        for index in tqdm(range(len(data))):
            weight_list = merge_weight(extract_weight(data, index))
            # print(data[index][0], merge_weight(extract_weight(data, index)))
            # iran_code = merg_iranecode(extract_irancode(data, index))
            avg_weight: int = 0
            tolerance: int = 8
            inserted_weights: int = 0
            valid_weights = []
            for weight in weight_list:
                if weight >= 8:
                    valid_weights.append(weight)

            if len(valid_weights) == 0:
                estimated_weight = int(estimate_weight(extract_name(data, index), qnts))
                if estimated_weight > 8:
                    avg_weight = estimated_weight
                    tolerance = int(max((avg_weight * 0.1), 8))
                    inserted_weights = 0
                    weight_list = [estimated_weight, 0, 0, 0, 0, 0, 0, 0, 0, 0]
            elif len(valid_weights) == 1:
                avg_weight = valid_weights[0]
                tolerance = int(max((avg_weight * 0.1), 8))
                inserted_weights = 0
                weight_list = [valid_weights[0], 0, 0, 0, 0, 0, 0, 0, 0, 0]
            elif len(valid_weights) == 2:
                if (
                    abs(valid_weights[0] - valid_weights[1])
                    / (valid_weights[0] + valid_weights[1])
                ) <= 0.1:
                    avg_weight = int((valid_weights[0] + valid_weights[1]) / 2)
                    tolerance = int(max((abs(valid_weights[0] - avg_weight)), 8))
                    inserted_weights = 2
                    weight_list = [
                        valid_weights[0],
                        valid_weights[1],
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                        0,
                    ]
                else:
                    estimated_weight = int(estimate_weight(extract_name(data, index), qnts))
                    temp_weights = merge_weight(
                        [valid_weights[0], valid_weights[1], estimated_weight]
                    )
                    if len(temp_weights) == 3:
                        avg_weight = int((valid_weights[0] + valid_weights[1]) / 2)
                        tolerance = int(max((abs(valid_weights[0] - avg_weight)), 8))
                        inserted_weights = 2
                        weight_list = [
                            valid_weights[0],
                            valid_weights[1],
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                            0,
                        ]
                    else:
                        if temp_weights[0] == estimated_weight:
                            avg_weight = temp_weights[1]
                            tolerance = int(max((avg_weight * 0.1), 8))
                            inserted_weights = 0
                            weight_list = [valid_weights[0], 0, 0, 0, 0, 0, 0, 0, 0, 0]
                        else:
                            avg_weight = temp_weights[0]
                            tolerance = int(max((avg_weight * 0.1), 8))
                            inserted_weights = 0
                            weight_list = [valid_weights[1], 0, 0, 0, 0, 0, 0, 0, 0, 0]
            else:
                avg_weight = int(sum(valid_weights) / len(valid_weights))
                tolerance = max(
                    (max(valid_weights) - avg_weight),
                    (avg_weight - min(valid_weights)),
                    (avg_weight * 0.1),
                    8,
                )
                inserted_weights = len(valid_weights)

            query = "EXEC Insert_Weights @Barcode= ?, @w1 = ?, @w2 = ?, @w3 = ?, @w4 = ?, @w5 = ?, @w6 = ?, @w7 = ?, @w8 = ?, @w9 = ?, @w10 = ?, @mean = ?, @tolerance = ?, @insertedweight = ?, @irancode = ?"
            params = (
                data[index][0],
                weight_list[0],
                weight_list[1],
                weight_list[2],
                weight_list[3],
                weight_list[4],
                weight_list[5],
                weight_list[6],
                weight_list[7],
                weight_list[8],
                weight_list[9],
                avg_weight,
                tolerance,
                inserted_weights,
                "",
            )

            print(params)
            with conn2.cursor() as cur:
                cur.execute(query, params)
            # print("update weights for barcode: ", data[index][0], index)

    conn2.close()
    exit()
