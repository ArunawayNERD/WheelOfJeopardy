using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Category
{
	public string name;
	public List<Question> questions = new List<Question>();

	public Category(string name) {
		this.name = name;
	}
	public void setQuestion(Question question) {
		this.questions.Add(question);
	}
	public List<Question> getQuestion() {
		return this.questions;
	}
}