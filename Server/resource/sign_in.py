from flask_restful import Resource,request
from helper.db import qlnx as db
from werkzeug.security import safe_str_cmp

def  validatePassword(gmail, password):
  '''
    sign in using gmail and passwork.
    return true when data is available
  '''
  return True

class ApiSignIn:
  @jwt_required
  def post(self):
    gmail = request.form['gmail']
    password = request.form['password']
    if validatePassword( gmail=gmail, password=password):
      return 200
    return 200