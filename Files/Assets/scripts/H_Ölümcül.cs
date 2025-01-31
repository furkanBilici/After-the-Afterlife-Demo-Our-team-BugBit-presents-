using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Ölümcül : MonoBehaviour
{
    public float cap = 2f;
    public float aci=2f;

    float time;
    // public int a;//top için 0 testere için 1 peri için 2 burası perinin sağa sola dönmesi için var y 0 dan büyükse b z rotasyonu 180 yap
    private void Start()
    {
        
        time = 0;
    }
    private void Update()
    {
        time+= Time.deltaTime;  
    }
    void FixedUpdate()
    {
        
        Movement();
    }

    public void Movement()
    {
        // Dairesel hareket için x ve y eksenlerini güncelle
        float x = Mathf.Cos(aci*time) * cap;
        float y = Mathf.Sin(aci*time) * cap;
       
        // Yeni pozisyonu ayarla
        Vector3 yeniPozisyon = new Vector3(this.transform.position.x + x, this.transform.position.y+y, this.transform.position.z);
        transform.position = yeniPozisyon;
    }
}
