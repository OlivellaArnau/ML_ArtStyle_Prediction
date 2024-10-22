import os
import shutil

def mover_imagenes(carpeta_origen, carpeta_destino):
    # Crear la carpeta de destino si no existe
    if not os.path.exists(carpeta_destino):
        os.makedirs(carpeta_destino)

    # Mover todas las imágenes de la carpeta de origen a la de destino
    for archivo in os.listdir(carpeta_origen):
        if archivo.endswith(('.png', '.jpg', '.jpeg', '.gif', '.bmp')):  # Extensiones de imagen
            shutil.move(os.path.join(carpeta_origen, archivo), carpeta_destino)
            print(f"Moviendo {archivo} a {carpeta_destino}")

# Uso
carpeta_origen = "E:\Documentos\Estudios\Erasmus\ML_Project_Artstyle_Prediction\ML_ArtStyle_Prediction\Scripts\images_abstract"  # Reemplaza con la ruta de tu carpeta de origen
carpeta_destino = "E:\Documentos\Estudios\Erasmus\ML_Project_Artstyle_Prediction\ML_ArtStyleTrainingData\Ml_Testing_Data\Abstract"  # Reemplaza con la ruta de tu carpeta de destino
mover_imagenes(carpeta_origen, carpeta_destino)
