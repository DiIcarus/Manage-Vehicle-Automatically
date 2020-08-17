from flask_restful import Resource,request
import datetime
from helper.db import qlnx as db

def validateForm(vehicle_id):
  result = db.selectTable(
    "SELECT * FROM VEHICLES WHERE ID_VEHICLES={vehicle_id}"
  )
  if len(result) >0:
    return True
  else:
    return False

class ApiRegisterMonthTicket:
  def post(sefl):
    vehicle_id = request.form['vehicle_id']
    date = datetime.datetime.now()
    if validateForm(vehicle_id=vehicle_id):
      db.insertTicket(
        vehicle_id=vehicle_id,
        date=date
      )
      return {
        "status":200,
        "message":"register successfully"
      }
    else:
      return{
        "status":400,
        "message": "vehicle_id not found"
      }