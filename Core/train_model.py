import numpy as np
from PIL import Image
import os
from sklearn.model_selection import train_test_split
from keras.utils import to_categorical
from keras.models import Sequential
from keras.layers import Conv2D, MaxPool2D, Dense, Flatten, Dropout


ds_label = ['0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z']
count_labels = len(ds_label)
cur_path = os.getcwd()
DATASET_PATH = 'Core/dataset_characters'
MODEL_NAME = './Core/model/character-model.h5'
EPOCHS=10

def load_dataset():
  data = []
  labels =[]
  for i in range(len(ds_label)):
    path = os.path.join(cur_path,DATASET_PATH,ds_label[i])
    images = os.listdir(path)
    for a in images:
      try:
        image = Image.open(path + '/' + a).convert('RGB')
        image = image.resize((30,30))
        image = np.array(image)
        data.append(image)
        labels.append(i)
      except:
        print("Error loading img")
  data= np.array(data)
  labels = np.array(labels)
  print("data",data.shape)
  print("labels",labels.shape)
  return data,labels


def div_data_from_dataset(data,labels):
  X_train, X_test, y_train, y_test = train_test_split(data,labels, test_size=0.2,random_state=42)
  y_train = to_categorical(y_train,count_labels)
  y_test = to_categorical(y_test,count_labels)
  return X_train, X_test, y_train, y_test

def get_model(X_train):
  model = Sequential()
  model.add(Conv2D(filters=32, kernel_size=(5,5), activation='relu', input_shape=X_train.shape[1:]))
  model.add(Conv2D(filters=64, kernel_size=(3, 3), activation='relu'))
  model.add(MaxPool2D(pool_size=(2, 2)))
  model.add(Dropout(rate=0.25))
  model.add(Conv2D(filters=64, kernel_size=(3, 3), activation='relu'))
  model.add(MaxPool2D(pool_size=(2, 2)))
  model.add(Dropout(rate=0.25))
  model.add(Flatten())
  model.add(Dense(256, activation='relu'))
  model.add(Dropout(rate=0.5))
  model.add(Dense(count_labels, activation='softmax'))
  model.compile(loss='categorical_crossentropy',optimizer='adam',metrics=['accuracy'])
  return model

def train_model(model,X_train, X_test, y_train, y_test):
  history=model.fit(X_train,y_train,batch_size=32,epochs=EPOCHS,validation_data=(X_test,y_test))
  model.save(MODEL_NAME)

def main():
  data,labels = load_dataset()
  X_train, X_test, y_train, y_test = div_data_from_dataset(data,labels)
  model = get_model(X_train)
  train_model(model,X_train, X_test, y_train, y_test)

if __name__ == "__main__":
  main()
