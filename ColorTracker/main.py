import cvzone
from cvzone.ColorModule import ColorFinder
import cv2
import socket  # Send data to Unity

# Start webcam
#cap = cv2.VideoCapture(0)
cap = cv2.VideoCapture(1, cv2.CAP_DSHOW)  # Camera index in Windows
cap.set(3, 1280)
cap.set(4, 720)

success, img = cap.read()
h, w, _ = img.shape

# Detect object color
myColorFinder_rosa = ColorFinder(False)  # Track pink device
hsvVals_rosa = {'hmin': 155, 'smin': 24, 'vmin': 165, 'hmax': 169, 'smax': 203, 'vmax': 255}  # Pink

myColorFinder_verde = ColorFinder(False)  # Track green device
hsvVals_verde = {'hmin': 25, 'smin': 45, 'vmin': 47, 'hmax': 91, 'smax': 255, 'vmax': 188}  # Green

# Sockets to send information
sock_rosa = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort_rosa = ("127.0.0.1", 5053)  # Pink object address

sock_verde = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort_verde = ("127.0.0.1", 5054)  # Green object adress

while True:
    success, img = cap.read()

    # Flipping the camera image horizontally to correct mirroring
    img = cv2.flip(img, 1)

    # Tracking Pink object
    imgColor_rosa, mask_rosa = myColorFinder_rosa.update(img, hsvVals_rosa)
    imgContour_rosa, contours_rosa = cvzone.findContours(img, mask_rosa)

    if contours_rosa:
        data_rosa = contours_rosa[0]['center'][0], \
                    h - contours_rosa[0]['center'][1], \
                    int(contours_rosa[0]['area'])
        print("Pink:", data_rosa)
        sock_rosa.sendto(str.encode(str(data_rosa)), serverAddressPort_rosa)

    # Tracking Green object
    imgColor_verde, mask_verde = myColorFinder_verde.update(img, hsvVals_verde)
    imgContour_verde, contours_verde = cvzone.findContours(img, mask_verde)

    if contours_verde:
        data_verde = contours_verde[0]['center'][0], \
                       h - contours_verde[0]['center'][1], \
                       int(contours_verde[0]['area'])
        print("Green:", data_verde)
        sock_verde.sendto(str.encode(str(data_verde)), serverAddressPort_verde)

    # Displaying contour images for both objects
    imgContour_rosa_resized = cv2.resize(imgContour_rosa, (0, 0), None, 0.3, 0.3)
    imgContour_verde_resized = cv2.resize(imgContour_verde, (0, 0), None, 0.3, 0.3)

    cv2.imshow("Pink", imgContour_rosa_resized)
    cv2.imshow("Green", imgContour_verde_resized)

    cv2.waitKey(1)
