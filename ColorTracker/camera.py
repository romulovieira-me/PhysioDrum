import cv2

for i in range(10):
    cap = cv2.VideoCapture(i)
    if cap.isOpened():
        print(f"Câmera detectada no índice {i}")
        cap.release()
