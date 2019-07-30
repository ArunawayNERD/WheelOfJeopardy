using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question
{
	public string question;
	public string answer;
	public int points;
    public string category;

	public Question(string question, string answer, int points, string category) {
		this.question = question;
		this.answer = answer;
		this.points = points;
        this.category = category;
	}
}
