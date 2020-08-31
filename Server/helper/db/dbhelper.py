import mysql.connector
from mysql.connector import errorcode

config = {
  'user': 'root',
  'password': 'Di Icarus',
  'host': '127.0.0.1',
  'database': 'qlnx3',
  'raise_on_warnings': True
}

def connect():
  try:
    cnx = mysql.connector.connect(**config)
    return cnx
  except mysql.connector.Error as err:
    if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
      print("Something is wrong with your user name or password")
    elif err.errno == errorcode.ER_BAD_DB_ERROR:
      print("Database does not exist")
    else:
      print(err)
    cnx.close()
    return None

def disconnect(cnx):  
  cnx.close()
  # cursor.close()

def select(cnx,querystr=("SELECT gmail,name,password FROM users")):
  if cnx is not None:
    cursor = cnx.cursor()
    cursor.execute(querystr)
    return cursor

def excute(cnx,querystr,  data):
  if cnx is not None:
    cursor = cnx.cursor()
    cursor.execute(querystr, data)
    cnx.commit()
def delete(cnx,querystr):
  if cnx is not None:
    cursor = cnx.cursor()
    cursor.execute(querystr)
    cnx.commit()

# add_employee = ("INSERT INTO employees "
#               "(first_name, last_name, hire_date, gender, birth_date) "
#               "VALUES (%s, %s, %s, %s, %s)")
# data_employee = ('Geert', 'Vanderkelen', 'date', 'M', date(1977, 6, 14))

# Insert new employee
# cursor.execute(add_employee, data_employee)
# emp_no = cursor.lastrowid

# str_query = ("INSERT INTO users "
#               "(id_users, gmail, phone_number, dob, password, name) "
#               "VALUES (%(id_users)s, %(gmail)s, %(phone_number)s, %(dob)s, %(password)s, %(name)s)")
# data_query = {
#   'id_users': 'emp_no1',
#   'gmail': 50000,
#   'phone_number': 'date',
#   'dob': '2020-10-10',
#   'password':'password',
#   'name':'name'
# }