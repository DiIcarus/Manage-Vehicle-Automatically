from flask_restful import Resource,request
from helper.db import qlnx as db
from uuid import uuid4

class ApiRegister(Resource):

  def post(self):
    id_users = str(uuid4()).split('-')[0]
    gmail = request.form['gmail']
    phone_number = request.form['phone_number']
    dob = request.form['dob']
    password = request.form['password']
    name = request.form['name']
    id_owners = str(uuid4()).split('-')[0]
    private_code = str(uuid4()).split('-')[0]
    public_code = str(uuid4()).split('-')[0]
    id_vehicles=request.form['id_vehicles']
    try:
      db.insertUser(
        id_users=id_users,
        gmail=gmail,
        phone_number=phone_number,
        dob=dob,
        password=password,
        name=name
      )
      db.insertOwner(
        id_owners=id_owners,
        user_id=id_users,
        private_code=private_code,
        public_code=public_code
      )
      db.insertVehicle(
        id_vehicles=id_vehicles,
        id_owner=id_owners
      )
    except:
      return "400 Error"
    print(db.selectTable(("SELECT * from vehicles")))
    return "200"
