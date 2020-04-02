using System;

namespace OneSpanSign.Sdk
{
	public enum SigningStatus
	{
		INACTIVE,
		COMPLETE,
		ARCHIVED,
		DECLINED,
		OPTED_OUT,
		EXPIRED,
		SIGNING_PENDING,
		SIGNING_COMPLETE,
        DELETED
	}
}