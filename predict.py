import numpy as np
import pyodbc
from sklearn.svm import SVC
import scipy

cnxn = pyodbc.connect("Driver={SQL Server Native Client 11.0};"
                      "Server=DESKTOP-4HP9J5D\\SQLEXPRESS;"
                      "Database=DatabaseFenea;"
                      "Trusted_Connection=yes;")

cursor = cnxn.cursor()
cursor.execute('SELECT * FROM Houses')

data = []
prices = []
zone = set()

for row in cursor:
    for i in range(16):
        if row[5] is not None:
            zone.add(str(row[5].encode('ascii', 'ignore')))
zone_index = {}
k = 0
for zona in zone:
    zone_index[zona] = k
    k += 1

cursor = cnxn.cursor()
cursor.execute('SELECT * FROM Houses')
for row in cursor:
    row_data = []
    #floor
    if row[2] is not None and isinstance(row[2], int):
        row_data.append(row[2])
    else:
        row_data.append(0)
    #LivingSurface
    if row[4] is not None and isinstance(row[4],int):
        row_data.append(row[4])
    else:
        row_data.append(0)

    #Neighborhood
    if row[5] is not None:
        row_data.append(zone_index[str(row[5].encode('ascii', 'ignore'))])
    else:
        row_data.append(0)

    #NumberOfBathrooms
    if row[6] is not None and isinstance(row[6], int):
        row_data.append(row[6])
    else:
        row_data.append(0)

    #NumberOfParkingSpaces
    if row[7] is not None and isinstance(row[7], int):
        row_data.append(row[7])
    else:
        row_data.append(0)

    #NumberOfRooms
    if row[8] is not None:
        row_data.append(int(row[8]))
    else:
        row_data.append(0)

    #year
    if row[15] is not None:
        row_data.append(int(row[15]))
    else:
        row_data.append(0)

    #price
    if row[11] is not None and isinstance(row[11], int):
        prices.append(row[11])
    else:
        prices.append(0)

    data.append(row_data)
print data
print prices
print len(zone_index)
print zone_index



X = np.array([[-1, -1], [-2, -1], [1, 1], [2, 1]])
y = np.array([1, 1, 2, 2])

clf = SVC()
clf.fit(data, prices)

'''
How to test: insert an array of 7 elements :
    floor,
    LivingSurface, 
    Neighborhood (folosind zone_index['nume zona']),
    NumberOfBathrooms,
    NumberOfParkingSpaces,
    NumberOfRooms,
    year
'''

print(clf.predict([[2, 45, 2, 1, 0, 2, 1988]]))
print()