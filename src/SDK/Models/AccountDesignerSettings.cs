using System;
using Newtonsoft.Json;
namespace OneSpanSign.API
{
	internal class AccountDesignerSettings
	{

		[JsonProperty("send")]
		public Nullable<Boolean> Send
		{
			get; set;
		}

		[JsonProperty("done")]
		public Nullable<Boolean> Done
		{
			get; set;
		}

		[JsonProperty("settings")]
		public Nullable<Boolean> Settings
		{
			get; set;
		}

		[JsonProperty("documentVisibility")]
		public Nullable<Boolean> DocumentVisibility
		{
			get; set;
		}

		[JsonProperty("addDocument")]
		public Nullable<Boolean> AddDocument
		{
			get; set;
		}

		[JsonProperty("editDocument")]
		public Nullable<Boolean> EditDocument
		{
			get; set;
		}

		[JsonProperty("deleteDocument")]
		public Nullable<Boolean> DeleteDocument
		{
			get; set;
		}

		[JsonProperty("addSigner")]
		public Nullable<Boolean> AddSigner
		{
			get; set;
		}

		[JsonProperty("editRecipient")]
		public Nullable<Boolean> EditRecipient
		{
			get; set;
		}


		[JsonProperty("rolePickerSender")]
		public Nullable<Boolean> RolePickerSender
		{
			get; set;
		}

		[JsonProperty("saveLayout")]
		public Nullable<Boolean> SaveLayout
		{
			get; set;
		}

		[JsonProperty("applyLayout")]
		public Nullable<Boolean> ApplyLayout
		{
			get; set;
		}

		[JsonProperty("showSharedLayouts")]
		public Nullable<Boolean> ShowSharedLayouts
		{
			get; set;
		}

		[JsonProperty("defaultSignatureType")]
		public String DefaultSignatureType
		{
			get; set;
		}
		
	}
}
