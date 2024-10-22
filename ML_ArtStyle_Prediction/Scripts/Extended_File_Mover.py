import os
import shutil

def mover_imagenes_con_intervalo(carpeta_origen, carpeta_destino, intervalo, cantidad_a_mover):
    # Crear la carpeta de destino si no existe
    if not os.path.exists(carpeta_destino):
        os.makedirs(carpeta_destino)

    # Contador para imágenes movidas
    contador_movidas = 0

    # Mover imágenes según el intervalo
    for idx, archivo in enumerate(os.listdir(carpeta_origen)):
        if archivo.endswith(('.png', '.jpg', '.jpeg', '.gif', '.bmp')):  # Extensiones de imagen
            if (idx + 1) % intervalo == 0 and contador_movidas < cantidad_a_mover:
                shutil.move(os.path.join(carpeta_origen, archivo), carpeta_destino)
                print(f"Moviendo {archivo} a {carpeta_destino}")
                contador_movidas += 1

            # Rompe el bucle si ya se han movido las imágenes deseadas
            if contador_movidas >= cantidad_a_mover:
                break

# Uso
carpeta_origen = "E:\Documentos\Estudios\Erasmus\ML_Project_Artstyle_Prediction\ML_ArtStyle_Prediction\Scripts\images_surrealism"  # Reemplaza con la ruta de tu carpeta de origen
carpeta_destino = "E:\Documentos\Estudios\Erasmus\ML_Project_Artstyle_Prediction\ML_ArtStyleTrainingData\ML_Feeding_Data\Surrealism"  # Reemplaza con la ruta de tu carpeta de destino
intervalo = 4  # Mover una imagen cada 3
cantidad_a_mover = 45  # Total de imágenes a mover
mover_imagenes_con_intervalo(carpeta_origen, carpeta_destino, intervalo, cantidad_a_mover)