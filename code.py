import pyodbc
import numpy as np

import matplotlib.pyplot as plt

cnxn = pyodbc.connect("Driver={SQL Server Native Client 11.0};"
                      "Server=DESKTOP-4HP9J5D\\SQLEXPRESS;"
                      "Database=DatabaseFenea;"
                      "Trusted_Connection=yes;")

cursor = cnxn.cursor()
cursor.execute('SELECT * FROM Houses')
k = 0
f = open("edges.csv", 'w')
columns = [column[0] for column in cursor.description]
print columns
'''
for column in columns:
    f.write(column)
    f.write(",")
f.write('\n')
'''
data = []
camere = [0]*7
print len(columns)
'''
for row in cursor:

   
    for i in range(16):
        if row[i] is not None:
            if isinstance(row[i], int):
                f.write(str(row[i]))
            else:
                f.write(str(row[i].encode('ascii', 'ignore')))
        else:
            f.write("-")
        if i <15:
            f.write(',')
        else:
            f.write('\n')

'''
zone = set()
for row in cursor:

    if row[0] is not None and row[5] is not None:
        f.write(str(row[0].encode('ascii', 'ignore')))
        f.write(',')
        f.write(str(row[5].encode('ascii', 'ignore')))
        f.write('\n')
        zone.add(str(row[5].encode('ascii', 'ignore')))




f1 = open('zone.csv', "w")

for zona in zone:
    f1.write(zona)
    f1.write('\n')


x = range(7)
width = 1/1.5
plt.bar(x, camere, width, color="blue")
plt.show()

print k


