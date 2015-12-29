#pragma once

namespace DRAG_MSG
{

	__declspec(selectany) UINT gMSG_DragEnter = ::RegisterWindowMessage(L"{1C80CEB1-3411-416f-84A7-DF20A3A65E19}");
	__declspec(selectany) UINT gMSG_DragOver  = ::RegisterWindowMessage(L"{24F8405F-BB3A-455e-B5B3-87220CD2E244}");
	__declspec(selectany) UINT gMSG_DragLeave = ::RegisterWindowMessage(L"{AC6822FA-05D6-473b-BAE6-A5C2E0084549}");
	__declspec(selectany) UINT gMSG_Drop      = ::RegisterWindowMessage(L"{826C229B-7EF0-4d33-AA49-D02117D188CA}");

}