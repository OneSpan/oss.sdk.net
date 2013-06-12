using System;
using NUnit.Framework;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
	public class FieldBuilderTest
	{
		private static double DEFAULT_DOUBLE_TOLERANCE = 0.01f;

		[Test]
		public void BuildsFieldWithDefaultValues() 
		{
			Field field = FieldBuilder.NewField ().AtPosition (100, 125).Build ();

			Assert.AreEqual (FieldBuilder.DEFAULT_WIDTH, field.Width);
			Assert.AreEqual (FieldBuilder.DEFAULT_HEIGHT, field.Height);
			Assert.AreEqual (FieldBuilder.DEFAULT_STYLE, field.Style);
		}

		[Test]
		public void BuildsFieldWithSpecifiedValues()
		{
			Field field = FieldBuilder.NewField ()
				.AtPosition (100, 125)
				.OnPage (2)
				.WithSize(75, 80)
				.WithStyle(FieldStyle.UNBOUND_CHECK_BOX)
				.Build ();

			Assert.AreEqual (100, field.X);
			Assert.AreEqual (125, field.Y);
			Assert.AreEqual (75, field.Width);
			Assert.AreEqual (80, field.Height);
			Assert.AreEqual (FieldStyle.UNBOUND_CHECK_BOX, field.Style);
			Assert.AreEqual (2, field.Page);
		}

		[Test]
		[ExpectedException(typeof(EslException))]
		public void CannotCreateFieldWithoutPosition() {
			FieldBuilder.SignatureDate().Build();
		}

		[Test]
		public void CreatingNewSignatureDateFieldSetsStyle() {
			Field field = FieldBuilder.SignatureDate().AtPosition(100, 100).Build();

			Assert.AreEqual (FieldStyle.BOUND_DATE, field.Style);
		}

		[Test]
			public void creatingNewSignerNameFieldSetsStyle() {
			Field field = FieldBuilder.SignerName().AtPosition(100, 100).Build();

			Assert.AreEqual (FieldStyle.BOUND_NAME, field.Style);
		}

		[Test]
			public void creatingNewSignerTitleFieldSetsStyle() {
			Field field = FieldBuilder.SignerTitle().AtPosition(100, 100).Build();

			Assert.AreEqual (FieldStyle.BOUND_TITLE, field.Style);
		}

		[Test]
			public void creatingNewSignerCompanyFieldSetsStyle() {
			Field field = FieldBuilder.SignerCompany().AtPosition(100, 100).Build();

			Assert.AreEqual (FieldStyle.BOUND_COMPANY, field.Style);
		}

		[Test]
			public void creatingTextFieldSetsStyle() {
			Field field = FieldBuilder.TextField().AtPosition(100, 100).Build();

			Assert.AreEqual (FieldStyle.UNBOUND_TEXT_FIELD, field.Style);
		}

		[Test]
			public void creatingCheckBoxFieldSetsStyle() {
			Field field = FieldBuilder.CheckBox().AtPosition(100, 100).Build();

			Assert.AreEqual (FieldStyle.UNBOUND_CHECK_BOX, field.Style);
		}
	}
}