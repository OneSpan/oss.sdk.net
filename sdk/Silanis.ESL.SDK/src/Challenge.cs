using System;

namespace Silanis.ESL.SDK
{
	public class Challenge
	{
		private readonly string question;
		private readonly string answer;

		public Challenge (string question, string answer)
		{
			this.question = question;
			this.answer = answer;
		}

		public string Question
		{
			get
			{
				return question;
			}
		}

		public string Answer
		{
			get
			{
				return answer;
			}
		}

		public override bool Equals(object other)
		{
			if (this == other)
			{
				return true;
			}

			if (other == null || other.GetType () != GetType ())
			{
				return false;
			}

			Challenge challenge = (Challenge)other;
			return challenge.Question.Equals (question) && challenge.Answer.Equals (answer);
		}

		public override int GetHashCode()
		{
			int hash = question.GetHashCode ();

			hash = hash * 31 + answer.GetHashCode ();
			return hash;
		}

		public override string ToString ()
		{
			return String.Format ("{0}, {1}", question, answer);
		}
	}
}