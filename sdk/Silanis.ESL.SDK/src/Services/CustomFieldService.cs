using System;
using Silanis.ESL.SDK.Internal;
using Silanis.ESL.SDK.Builder;
using Newtonsoft.Json;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK.Services
{
	public class CustomFieldService
	{
        private CustomFieldApiClient apiClient;
        
        internal CustomFieldService(CustomFieldApiClient apiClient)
        {
            this.apiClient = apiClient;
        }
        
		///
		/// Create an account custom field.
		/// If the custom field already existed then update it.
		///
		/// @param customField
		/// @return the custom field added created or updated
		/// @throws com.silanis.esl.sdk.EslException
		///
		public CustomField CreateCustomField(CustomField customField)
		{
            Silanis.ESL.API.CustomField apiCustomField = new CustomFieldConverter( customField ).ToAPICustomField();
            apiCustomField = apiClient.CreateCustomField( apiCustomField );
            return new CustomFieldConverter(apiCustomField).ToSDKCustomField();
		}

		///
		/// Determine if a custom field already existed
		///
		/// @param id of custom field
		/// @return true if existed otherwise false
		///
		public bool DoesCustomFieldExist(string customFieldId)
		{
            return apiClient.DoesCustomFieldExist(customFieldId);
		}

		/// <summary>
		/// Delete an account custom field.
		/// </summary>
		/// <param name="id">id of custom field to delete.</param>
		public void DeleteCustomField(string id)
		{
            apiClient.DeleteCustomField(id);
		}

		///
		/// Create an user custom field.
		/// If the custom field already existed then update it.
		///
		///
		/// @param customFieldValue
		/// @return user custom field id
		/// @throws com.silanis.esl.sdk.EslException
		///
		public CustomFieldValue SubmitCustomFieldValue(CustomFieldValue customFieldValue)
        {   
            UserCustomField apiCustomFieldValue = customFieldValue.toAPIUserCustomField();
            apiCustomFieldValue = apiClient.SubmitCustomFieldValue(apiCustomFieldValue);
            return CustomFieldValueBuilder.CustomFieldValue(apiCustomFieldValue).build();
        }

		///
		/// Determine if an user custom field already existed
		///
		/// @param id of user custom field
		/// @return true if existed otherwise false
		///
		public bool DoesCustomFieldValueExist(string customFieldId)
		{
            return apiClient.DoesCustomFieldValueExist( customFieldId );
		}
	}
}

