using System;

namespace OneSpanSign.Sdk
{
	public class Challenge
	{
		public enum MaskOptions
		{
			MaskInput, None
		}

		private readonly string question;
		private readonly string answer;
		private readonly MaskOptions maskOption;
		private readonly string challengeType;


		public Challenge (string question, string answer) : this(question, answer, MaskOptions.None)
		{
		}

		public Challenge (string question, string answer, MaskOptions maskOption)
		{
			this.question = question;
			this.answer = answer;
			this.maskOption = maskOption;
		}
		
		public Challenge (string question, string answer, string challengeType)
		{
			this.question = question;
			this.answer = answer;
			this.challengeType = challengeType;
			this.maskOption = MaskOptions.None;
		}
		
		public Challenge (string question, string answer, string challengeType, MaskOptions maskOption)
		{
			if (challengeType == null || challengeType.Trim().Length == 0) {
				throw new OssException("challenge type parameter cannot be null or empty", null);
			}
			if (question == null || question.Trim().Length == 0) {
				throw new OssException("question parameter cannot be null or empty", null);
			}

			this.question = question;
			this.answer = answer;
			this.challengeType = challengeType;
			this.maskOption = maskOption;
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

		public MaskOptions MaskOption
		{
			get
			{
				return maskOption;
			}
		}

		public string ChallengeType
		{
			get
			{
				return challengeType;
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