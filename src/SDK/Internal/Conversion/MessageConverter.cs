using System;
using OneSpanSign.API;
using System.Collections.Generic;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk
{
    internal class MessageConverter
    {
        private OneSpanSign.Sdk.Message sdkMessage = null;
        private OneSpanSign.API.Message apiMessage = null;

        public MessageConverter(OneSpanSign.Sdk.Message sdkMessage)
        {
            this.sdkMessage = sdkMessage;
        }

        public MessageConverter(OneSpanSign.API.Message apiMessage)
        {
            this.apiMessage = apiMessage;
        }

        public OneSpanSign.API.Message ToAPIMessage()
        {
            if (sdkMessage == null)
            {
                return apiMessage;
            }

            OneSpanSign.API.Message result = new OneSpanSign.API.Message();

            if (sdkMessage.Content != null)
            {
                result.Content = sdkMessage.Content;
            }

            if (sdkMessage.From != null)
            {
                Signer fromSigner = sdkMessage.From;
                User fromUser = new User();
                fromUser.Email = fromSigner.Email;
                fromUser.FirstName = fromSigner.FirstName;
                fromUser.LastName = fromSigner.LastName;
                fromUser.Id = fromSigner.Id;
                fromUser.Company = fromSigner.Company;
                fromUser.Title = fromSigner.Title;

                result.From = fromUser;
            }

            if (sdkMessage.To != null && sdkMessage.To.Count != 0)
            {
                foreach (Signer toSigner in sdkMessage.To.Values)
                {
                    User toUser = new User();
                    toUser.Email = toSigner.Email;
                    toUser.FirstName = toSigner.FirstName;
                    toUser.LastName = toSigner.LastName;
                    toUser.Company = toSigner.Company;
                    toUser.Title = toSigner.Title;

                    result.AddTo(toUser);
                }
            }

            if (sdkMessage.Created != null)
            {
                result.Created = sdkMessage.Created;
            }


            result.Status = new MessageStatusConverter(sdkMessage.Status).ToAPIMessageStatus();

            return result;
        }

        public OneSpanSign.Sdk.Message ToSDKMessage()
        {
            if (apiMessage == null)
            {
                return sdkMessage;
            }

            User fromUser = apiMessage.From;
            Signer fromSigner = SignerBuilder.NewSignerWithEmail(fromUser.Email)
                .WithCompany(fromUser.Company)
                .WithFirstName(fromUser.FirstName)
                .WithLastName(fromUser.LastName)
                .WithCustomId(fromUser.Id)
                .WithTitle(fromUser.Title)
                .Build();

            OneSpanSign.Sdk.Message result = new OneSpanSign.Sdk.Message(new MessageStatusConverter(apiMessage.Status).ToSDKMessageStatus(), apiMessage.Content, fromSigner);

            if (apiMessage.To != null && apiMessage.To.Count != 0)
            {
                foreach (User toUser in apiMessage.To)
                {
                    Signer to = SignerBuilder.NewSignerWithEmail(toUser.Email)
                        .WithCompany(toUser.Company)
                        .WithFirstName(toUser.FirstName)
                        .WithLastName(toUser.LastName)
                        .WithCustomId(toUser.Id)
                        .WithTitle(toUser.Title)
                        .Build();

                    result.AddTo(to);
                }
            }

            if (apiMessage.Created != null)
            {
                result.Created = apiMessage.Created;
            }

            return result;
        }
    }
}

