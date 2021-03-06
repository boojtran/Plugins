using UnityEngine;
using System.Collections;

public class TransportHandler : SaucerBaseHandler
{
	public CtxMenu commandSubmenu;
	CtxMenu.Item[] menuItems;
	
	enum Commands
	{
		LandAtWhiteHouse = 40,
		DeployGrays,
		RecoverGrays,
	}
	
	public void OnShowPopupMenu()
	{
		BuildMenu();
		CtxPopup.current.menuItems = menuItems;
	}
	
	public void OnShowMenu(CtxObject obj)
	{
		BuildMenu();
		obj.menuItems = menuItems;
	}
	
	public void OnHidePopupMenu()
	{
		Debug.Log("Transport menu hidden (popup)");
	}
	
	public void OnHideMenu(CtxObject obj)
	{
		Debug.Log("Transport menu hidden (object)");
	}
	
	void BuildMenu()
	{
		int baseItem = base.MenuItemCount;
		
		if (menuItems == null)
		{
			int itemCnt = baseItem+1;
			menuItems = new CtxMenu.Item[itemCnt];
			
			for (int i=0; i<itemCnt; i++)
				menuItems[i] = new CtxMenu.Item();
		}
		
		base.FillMenuItems(menuItems);
	
		menuItems[baseItem].text = "Transport";
		menuItems[baseItem].isSubmenu = true;
		menuItems[baseItem].submenu = commandSubmenu;
		
#if CTX_NO_SERIALIZATION_FIX
		CtxMenu.Item[] submenuItems = new CtxMenu.Item[3];
		menuItems[baseItem].submenuItems = submenuItems;
#else
		CtxMenu.Item[] submenuItems = new CtxMenu.Item[3];
		commandSubmenu.items = submenuItems;
#endif

		for (int i=0; i<3; i++)
		{
			submenuItems[i] = new CtxMenu.Item();
			submenuItems[i].id = (int)Commands.LandAtWhiteHouse+i;
		}
		
		submenuItems[0].text = "Land At White House";
		submenuItems[1].text = "Deploy Grays";
		submenuItems[2].text = "Recover Grays";
	}
	
	public new void OnMenuSelection()
	{
		int selection = CtxMenu.current.selectedItem;
		Commands cmd = (Commands)selection;
		switch (cmd)
		{
		case Commands.LandAtWhiteHouse:
		case Commands.DeployGrays:
		case Commands.RecoverGrays:
			Debug.Log("Transport: "+cmd.ToString());
			break;
		default:
			base.OnMenuSelection();
			break;
		}
	}
}
