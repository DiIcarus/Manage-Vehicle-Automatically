import sys, os
sys.path.append(os.path.abspath('.'))

from flask import Flask
from flask_cors import CORS
from flask_restful import Resource,Api
from flask_mail import Mail, Message
from flask_jwt_extended import JWTManager

from resource.register import ApiRegister
mail_setting = {
  "MAIL_SERVER": 'smtp.gmail.com',
  "MAIL_PORT": 465,
  "MAIL_USE_TLS": False,
  "MAIL_USE_SSL": True,
  "MAIL_USERNAME": 'nguyenlehaison4074@gmail.com',
  "MAIL_PASSWORD": 'Di Icarus'
}


def api_add_resource():
  global api
  class HelloWord(Resource):
    def get(self):
      return {'Hello':"world"}
  class SendGmail(Resource):
    def get(self,email):
      global mail
      global app
      message = Message(
        subject="",
        sender=app.config.get("MAIL_USERNAME"),
        recipients=[email],
        body="Flask QLNX send mail 1234"
      )
      mail.send(message)
      return "mail is send"
  
  api.add_resource(HelloWord,'/',)
  api.add_resource(SendGmail,'/<string:email>')
  api.add_resource(ApiRegister,'/register')
  
def main():
  global app
  global api
  global mail
  app = Flask(__name__)
  CORS(app,resources={r"/*": {"origins": "*"}})
  api = Api(app)
  api_add_resource()
  app.config.update(mail_setting)

  app.config['JWT_SECRET_KEY'] = 'JWT_SECRET_KEY'

  jwt = JWTManager(app)
  mail = Mail(app)
  app.run(port=5000)
  
if __name__=="__main__":
  main()