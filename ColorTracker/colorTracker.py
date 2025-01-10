import cvzone
from cvzone.ColorModule import  ColorFinder
import cv2

cap = cv2.VideoCapture(0)
cap.set(3, 1280)
cap.set(4, 720)

myColorFinder = ColorFinder(True)
hsvVals = {'hmin': 165, 'smin': 157, 'vmin': 159, 'hmax': 177, 'smax': 255, 'vmax': 255} #Rosa

while True:
    success, img = cap.read()
    imgColor, mask = myColorFinder.update(img, hsvVals)

    imgStack = cvzone.stackImages([img, imgColor], 2, 0.5)
    cv2.imshow("Image", imgStack)
    cv2.waitKey(1)