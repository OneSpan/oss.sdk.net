using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk.Builder
{
	public class QASMSBuilder : AuthenticationBuilder
	{
		private static string SMS_CHALLENGE_TYPE = "SMS";

		private string question;
		private string challengeType;
		private IList<Challenge> challenges = new List<Challenge>();

		private QASMSBuilder (string question, string challengeType)
		{
			this.question = question;
			this.challengeType = challengeType;
		}

		public static QASMSBuilder FirstQuestion(string challengeType, string question)
		{
			return new QASMSBuilder (question, challengeType);
		}

		public QASMSBuilder Answer(string answer)
		{
			return Answer(answer, Challenge.MaskOptions.None);
		}

        [Obsolete]
		public QASMSBuilder Answer(string answer, Challenge.MaskOptions maskOption)
		{
			challenges.Add (new Challenge(challengeType, question, answer, maskOption));
			return this;
		}

        public QASMSBuilder AnswerWithMaskInput(string answer)
        {
            challenges.Add (new Challenge(challengeType, question, answer, Challenge.MaskOptions.MaskInput));
            return this;
        }

		public QASMSBuilder SecondQuestion (string challengeType, string question)
		{
			this.question = question;
			this.challengeType = challengeType;
			return this;
		}

		public QASMSBuilder smsPhoneNumber(String question) {
			challenges.Add(new Challenge(question, null, SMS_CHALLENGE_TYPE,  Challenge.MaskOptions.None));
			return this;
		}
		
		public override Authentication Build()
		{
			if (QuestionProvided () && challenges.Count == 0)
			{
				throw new OssException ("Question challenge was provided with no answer",null);
			}
			if (ChallengeTypeProvided () && challenges.Count == 0)
			{
				throw new OssException ("Challenge type was provided with no challenge",null);
			}
			Authentication result = new Authentication(challenges);

			return result;
		}

		private bool QuestionProvided ()
		{
			return question != null && question.Trim ().Length > 0;			
		}
		private bool ChallengeTypeProvided() {
			return challengeType != null && challengeType.Trim().Length > 0;
		}

	}
}