from helper.db import dbhelper as db
def createArrayFromReturnCursor(cursor):
  arr = []
  for row in cursor:
    arr.append(row)
  cursor.close()
  return arr

def selectTable(querystr=""):
  '''
    return array(column1,column2, . . )
  '''
  cnx=db.connect()
  result = db.select(
    cnx=cnx,
    querystr=querystr
  )
  result = createArrayFromReturnCursor(result)
  db.disconnect(cnx)
  return result

def insertCheckIn(vehicle_id="",key_code="",time=""):
  querystr = (
    "INSERT INTO check-in"
    "(vehicle_id, key_code, time)"
    "value(%s,%s,%s)"
  )
  data = (vehicle_id,key_code,time)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=data,
    data=data
  )
  db.disconnect(cnx)

def insertUser(id_users="", gmail="", phone_number="", dob="", password="", name=""):
  querystr = (
    "INSERT INTO users"
    "(id_users, gmail, phone_number, dob, password, name)"
    "value(%s, %s, %s, %s, %s, %s)"
  )
  data = (id_users, gmail, phone_number, dob, password, name)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=querystr,
    data=data
  )
  db.disconnect(cnx)
  
def insertOwner(id_owners="", user_id="", private_code="", public_code=""):
  querystr = (
    "INSERT INTO Owners"
    "(id_owners, user_id, private_code, public_code)"
    "values(%s, %s, %s, %s)"
  )
  data = (id_owners, user_id, private_code, public_code)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=querystr,
    data=data
  )
  db.disconnect(cnx)

def insertCheckOut(vehicle_id="", key_code="", send_code="", time=""):
  querystr = (
    "INSERT INTO check-out"
    "(vehicle_id, key_code, send_code, time)"
    "value(%s, %s, %s, %s)"
  )
  data = (vehicle_id, key_code, send_code, time)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=querystr,
    data=data
  )
  db.disconnect(cnx)
def insertSharingCounter(orther_id="", name=""):
  querystr = (
    "INSERT INTO sharing_counter"
    "(orther_id, name)"
    "values(%s, %s)"
  )
  data = (orther_id, name)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=querystr,
    data=data
  )
  db.disconnect(cnx)

def insertVehiclesSharingCounter(vehicles_id_vehicles="", sharing_counter_id=0):
  querystr = (
    "INSERT INTO vehicles_sharing_counter"
    "(vehicles_id_vehicles, sharing_counter_id)"
    "values(%s,%s)"
  )
  data(vehicles_id_vehicles,sharing_counter_id)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=querystr,
    data = data
  )
  db.disconnect(cnx)

def insertVehicle(id_vehicles="",id_owner=""):
  querystr = (
    "INSERT INTO vehicles"
    "(id_vehicles, id_owner)"
    "values(%s,%s)"
  )
  data = (id_vehicles, id_owner)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr=querystr,
    data=data
  )
  db.disconnect(cnx)

def insertTicket(vehicle_id="",date=""):
  querystr = (
    "INSERT INTO ticket"
    "(vehicle_id, data)"
    "values(%s,%s)"
  )
  data = (vehicle_id, date)
  cnx = db.connect()
  db.excute(
    cnx=cnx,
    querystr= querystr,
    data=data
  )
  db.disconnect(cnx)
