from detector import detect_character
from recognition import identify_character
import cv2
import glob

IMAGE_PATHS = glob.glob("./Core/demo/*.jpg")
# PATH_IMAGE = './Core/demo/vietnam_car_rectangle_plate.jpg'

def main():
  for i in range(len(IMAGE_PATHS)):
    img = cv2.imread(IMAGE_PATHS[i])
    print('#PLATE',i)
    print('x,y,w,h')
    images = detect_character(img)
    result = []
    for im in images:
      result.append(identify_character(im))
    print(result)
if __name__ == "__main__":
  main()