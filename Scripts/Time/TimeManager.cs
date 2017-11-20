using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : DataBase {

    public static TimeManager instance = null;
    static public TimeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TimeManager();
                instance.isReady = EncryptionManager.GetInt("isReady", 0);
                instance.isReadyFuture = EncryptionManager.GetInt("isReadyFuture", 0);
                foreach (string key in instance.timeKeyArr)
                {
                    instance.timeDic.Add(key, EncryptionManager.GetInt(key, 1));
                }
                instance.updateTimeAll();
            }
            return instance;
        }
    }

    Dictionary<string, int> timeDic = new Dictionary<string, int>();
    public string[] timeKeyArr = {"year", "month","day", "hour", "minute", "second",
                                        "yearFuture", "monthFuture","dayFuture", "hourFuture", "minuteFuture", "secondFuture" };
	public string[] timeTextKeyArr = { "年", "月", "日", "时", "分", "秒" };
    
	public int isReady;
    public int isReadyFuture;
    System.DateTime inputTime;
    System.DateTime inputTimeFuture;

    public bool IsReady
    {
        get { return isReady != 0; }
    }
    public bool IsReadyFuture
    {
        get { return isReadyFuture != 0; }
    }
    
    public void updateTimeAll()
    {
        updateTime(out inputTime);
        updateTime(out inputTimeFuture, 6);
    }

    public void updateTime(out System.DateTime inputTime,int num = 0)
    {
        inputTime = new System.DateTime(getTimeByType(0 + num),
                                                                getTimeByType(1 + num),
                                                                getTimeByType(2 + num),
                                                                getTimeByType(3 + num),
                                                                getTimeByType(4 + num),
                                                                getTimeByType(5 + num)
                                                                );
    }

    public void setTimeByType (int timeType, int value) {
        string key = timeKeyArr[timeType];
        timeDic[key] = value;
        EncryptionManager.SetInt(key, value);
        updateTimeAll();
        if(timeType<6){
            if (isReady!=1){
                isReady = 1;
                EncryptionManager.SetInt("isReady", 1);
                EncryptionManager.Save();
            }
        }else {
            if(isReadyFuture!=1){
                isReadyFuture = 1;
                EncryptionManager.SetInt("isReadyFuture", 1);
                EncryptionManager.Save();
            }
        }
        EncryptionManager.Save();
		ReadySend();
    }

    public int getTimeByType (int timeType) {
        string key = timeKeyArr[timeType];
        return timeDic[key];
	}

    public double getPastTimeByType(int timeType)
    {
        System.DateTime nowTime = System.DateTime.Now;
        
        switch (timeType)
        {
            case 0:
                return getTotalYear(nowTime,inputTime);
            case 1:
                return getTotalMonth(nowTime,inputTime);
        }
        System.TimeSpan ts1 = new System.TimeSpan(nowTime.Ticks);
        System.TimeSpan ts2 = new System.TimeSpan(inputTime.Ticks);
        System.TimeSpan tsSub = ts1.Subtract(ts2).Duration();
        return getTimeByTimeSpan(tsSub, timeType);
    }

    public double getFutureTimeByType(int timeType)
    {
        System.DateTime nowTime = System.DateTime.Now;

        switch (timeType)
        {
            case 6:
                return getTotalYear(inputTimeFuture,nowTime);
            case 7:
                return getTotalMonth(inputTimeFuture,nowTime);
        }
        
        System.TimeSpan ts1 = new System.TimeSpan(nowTime.Ticks);
        System.TimeSpan ts2 = new System.TimeSpan(inputTimeFuture.Ticks);
        System.TimeSpan tsSub = ts2.Subtract(ts1).Duration();
        return getTimeByTimeSpan(tsSub, timeType-6);
    }
    public double getTimeByTimeSpan(System.TimeSpan tsSub, int timeType)
    {
        switch (timeType)
        {
            case 2:
                return tsSub.TotalDays;
            case 3:
                return tsSub.TotalHours;
            case 4:
                return tsSub.TotalMinutes;
            case 5:
                return tsSub.TotalSeconds;
        }
        return 0;
    }

    public double getTotalYear(System.DateTime time1,System.DateTime time2)
    {
        double year = time1.Year;
        double month = time1.Month;
        double day= time1.Day;
        double hour = time1.Hour;
        double minutes = time1.Minute;
        double seconds = time1.Second;

        double _Year = time2.Year;
        double _Month = time2.Month;
        double _Day= time2.Day;
        double _Hour = time2.Hour;
        double _Minute = time2.Minute;
        double _Second = time2.Second;
        double yearNum = year - _Year;
        if (_Month > month + 1)
        {
            yearNum--;
        }
        else if (_Month == month + 1)
        {
            if (_Day > day)
            {
                yearNum--;
            }
            else if (_Day == day)
            {
                if (_Hour > hour)
                {
                    yearNum--;
                }
                else if (_Hour == hour)
                {
                    if (_Minute > minutes)
                    {
                        yearNum--;
                    }
                    else if (_Minute == minutes)
                    {
                        if (_Second > seconds)
                        {
                            yearNum--;
                        }
                    }
                }
            }
        }
        return yearNum;
    }

    public double getTotalMonth(System.DateTime time1,System.DateTime time2){
        double year = time1.Year;
        double month = time1.Month;
        double day= time1.Day;
        double hour = time1.Hour;
        double minutes = time1.Minute;
        double seconds = time1.Second;

        double _Year = time2.Year;
        double _Month = time2.Month;
        double _Day= time2.Day;
        double _Hour = time2.Hour;
        double _Minute = time2.Minute;
        double _Second = time2.Second;
        double monthNum = (year - _Year) * 12 + month - _Month;

        if (_Day > day){
            monthNum--;
        }
        else if (_Day == day){
            if (_Hour > hour){
                monthNum--;
            }
            else if (_Hour == hour){
                if (_Minute > minutes){
                    monthNum--;
                }
                else if (_Minute == minutes){
                    if (_Second > seconds){
                        monthNum--;
                    }
                }
            }
        }
        return monthNum;
    }

    public int GetMonthMaxDay(int type, int year = 0, int month = 0)
    {
        
        if (type == 2)
        {
            if (year == 0)
                year = getTimeByType(0);
            if (month == 0)
                month = getTimeByType(1);
        }
        else
        {
            if (year == 0)
                year = getTimeByType(6);
            if (month == 0)
                month = getTimeByType(7);
        }
        if (month == 2)
        {
            if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
            {
                return 29;
            }
            else
            {
                return 28;
            }
        }
        else
        {
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                return 31;
            }
            else
            {
                return 30;
            }
        }
    }
}