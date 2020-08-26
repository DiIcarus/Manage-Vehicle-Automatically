from flask_restful import Resource,request
from helper.db import qlnx as db
from uuid import uuid4


class ApiRegister(Resource):

  def post(self):
    gmail = request.form['gmail']
    phone_number = request.form['phone_number']
    dob = request.form['dob']
    password = request.form['password']
    name = request.form['name']
    id_vehicles=request.form['id_vehicles']
    if db.Register(
      gmail=gmail,
      phone_number=phone_number,
      dob=dob,
      password=password,
      name=name,
      id_vehicles=id_vehicles
    ):
      print(db.selectTable(("SELECT * from vehicles")))
      return "200"
    else:
      return "400"