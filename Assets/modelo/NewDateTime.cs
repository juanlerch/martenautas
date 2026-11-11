using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class NewDateTime {
	public int second;
	public int minute;
	public int hour;

	public int day;
	public int month;
	public int year;

	public static NewDateTime CreateNewDateTime(DateTime dateTime) {
		NewDateTime newDateTime = new NewDateTime();
		newDateTime.second = dateTime.Second;
		newDateTime.minute = dateTime.Minute;
		newDateTime.hour = dateTime.Hour;
		newDateTime.day = dateTime.Day;
		newDateTime.month = dateTime.Month;
		newDateTime.year = dateTime.Year;

		return newDateTime;
	}

	public override string ToString() {
		return day + "/" + month + "/" + year + " at " + hour + ":" + minute + ":" + second;
	}

}