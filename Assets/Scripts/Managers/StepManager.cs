using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Simülasyon kısmının temel managerı. Step != frame.
/// </summary>
public class StepManager : MonoBehaviour
{
    public delegate void StepProgress();
    public static event StepProgress OnStepProgress;

    public DateTime appCloseDate;

    public bool isForeground; //App foreground'da mı? Değilse stepler durmalı.

    float msAccumulation;

    // Start is called before the first frame update
    void Start()
    {
        //İlk olarak offline kaybı gider. Bu app'ın kapatılıp yeniden açılması sırasında çağırılıyor.
        //Aslında buna gerek yok gibi. En azından Editör'de focusu play mode geçtiğimizde bir kere çağırıyor.
        //ProcessOfflineSteps(GetOfflineStepCount());
    }

    void Update()
    {
        msAccumulation += Time.deltaTime;

        if (msAccumulation >= 1f)
        {
            msAccumulation = 0f;
            DoStep();
        }
    }

    void DoStep()
    {
        //if (isForeground)
        //    OnStepProgress();
        OnStepProgress();
    }

    void ProcessOfflineSteps(int offlineSteps)
    {
        //Offline step sayısı kadar step processleyelim.
        //Çok büyük sayılar da ufak bir lag girme ihtimali var.
        for (int i = 0; i < offlineSteps; i++)
        {
            DoStep();
        }
    }

    //App'in focus kaybettiği tarihi alıp PP'ye kaydeder.
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            isForeground = false;
            appCloseDate = DateTime.Now;
            PlayerPrefs.SetString("appCloseDate", appCloseDate.ToString());

            //Debug.Log($"Focus lost at: {appCloseDate.ToString()}");
        }
        else //Focus'u geri aldığımızda offline kaybını giderelim. Bu background'da çalışırken geri app'e döndüğümüzde çağırılıyor.
        {
            isForeground = true;
            int offlineSteps = GetOfflineStepCount();
            ProcessOfflineSteps(offlineSteps);

            //Debug.Log($"Focus regained at: {DateTime.Now} with {offlineSteps} offline steps.");
        }
    }

    //App açıldığında son kapanma tarihini alıp şu anki tarihle farkını hesaplar.
    //Buna göre de offline geçen step sayısını bulur.
    int GetOfflineStepCount()
    {
        try
        {
            appCloseDate = DateTime.Parse(PlayerPrefs.GetString("appCloseDate"));
        }
        catch (Exception)
        {
            appCloseDate = DateTime.Now;
            //Debug.Log("No appCloseDate found.");
        }

        DateTime now = DateTime.Now;
        TimeSpan difference = now - appCloseDate;
        int stepCount = (int)difference.TotalSeconds;
        //Debug.Log($"Offline step Count: {stepCount}");
        return stepCount;
    }

    private void OnDisable()
    {
        //Burası playerPrefs'i temizlemek için. Play mode her seferinde PP'yi refreshlemiyor :/
        if (EditorApplication.isPlaying)
        {
            //Debug.Log("Play mode is closing.");
            PlayerPrefs.DeleteAll();
        }
    }
}
