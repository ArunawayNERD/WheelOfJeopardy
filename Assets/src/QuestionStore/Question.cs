using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
	public string title;
	public string answer;
	public string points;

	public Question(string title, string answer, string points) {
		this.title = title;
		this.answer = answer;
		this.points = points;
	}
}
