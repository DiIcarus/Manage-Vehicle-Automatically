import mysql.connector
from mysql.connector import errorcode
from config import config

config = {
  'user': 'root',
  'password': 'Di Icarus',
  'host': '127.0.0.1',
  'database': 'qlnx',
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
  
def select():
  cnx = connect()
  if cnx is not None:
    cursor = cnx.cursor()

    query = ("SELECT gmail,name,password FROM users")

    cursor.execute(query)

    for (gmail, name, password) in cursor:
      # print("{}, {} was hired on {:%d %b %Y}".format(
      #   name, gmail, password))
      print(name,gmail,password)

    cursor.close()
    cnx.close()

def insert():
  cnx = connect()
  if cnx is not None:
    cursor = cnx.cursor()

    # add_employee = ("INSERT INTO employees "
    #               "(first_name, last_name, hire_date, gender, birth_date) "
    #               "VALUES (%s, %s, %s, %s, %s)")
    # data_employee = ('Geert', 'Vanderkelen', 'date', 'M', date(1977, 6, 14))

    # # Insert new employee
    # cursor.execute(add_employee, data_employee)
    # emp_no = cursor.lastrowid

    str_query = ("INSERT INTO users "
                  "(id_users, gmail, phone_number, dob, password, name) "
                  "VALUES (%(id_users)s, %(gmail)s, %(phone_number)s, %(dob)s, %(password)s, %(name)s)")
    data_query = {
      'id_users': 'emp_no1',
      'gmail': 50000,
      'phone_number': 'date',
      'dob': '2020-10-10',
      'password':'password',
      'name':'name'
    }
    cursor.execute(str_query, data_query)

    cnx.commit()

    cursor.close()
    cnx.close()
def update():
  cnx = connect()
  if cnx is not None:
    cursor = cnx.cursor()

    str_query = ("UPDATE users "
                  "SET name= %(name)s, gmail= %(gmail)s, password=%(password)s "
                  "WHERE id_users='emp_no1'")
    data_query = {
      'gmail': 6666,
      'password':'Di Icarus',
      'name':'HS'
    }
    cursor.execute(str_query, data_query)

    cnx.commit()

    cursor.close()
    cnx.close()
# insert()
select()
update()
select()