using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "SaveManager", menuName = "ScriptableObject/SaveManager", order = 0)]
public class SaveManager : ScriptableObject
{
    public int a = 0;
    

    public SaveManager()
    {

    }

    // Checkpoint kaydetme fonksiyonu
    public void CheckpointSave(Vector3 lastCheckpoint)
    {
     
        PlayerPrefs.SetInt("konumX", Convert.ToInt32(lastCheckpoint.x));
        PlayerPrefs.SetInt("konumY", Convert.ToInt32(lastCheckpoint.y));  // 'konumy' yerine 'konumY' kullanýldý
        PlayerPrefs.Save();
    }

    // Checkpoint yükleme fonksiyonu
    public Vector3 CheckpointLoad()  // 'ChechPointLoad' -> 'CheckpointLoad' olarak düzeltildi
    {
        // Konumun daha önce kaydedilip edilmediðini kontrol et
        if (PlayerPrefs.HasKey("konumX") && PlayerPrefs.HasKey("konumY"))
        {
            return new Vector3(PlayerPrefs.GetInt("konumX"), PlayerPrefs.GetInt("konumY"), 0);
        }
        else
        {
            // Eðer kayýtlý konum yoksa varsayýlan deðer döndür
            return new Vector3(0, 0, 0);
        }
    }

    // Ýleride kullanýlabilecek bir fonksiyon þablonu
    void CheckPointtenmi()
    {
        // Bu fonksiyon içeriði doldurulabilir
    }
}