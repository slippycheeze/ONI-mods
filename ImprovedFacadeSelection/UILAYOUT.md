<!-- -*- truncate-lines: t; -*- -->

randomly, a version with an interstitial ScrollRect in it.  OMG.

```
21:14:16.7299 M1  SC:E ImprovedFacadeSelectionPanel.OnPrefabInit] ImprovedFacadeSelectionPanel(FacadeSelectionPanel(Clone)[-6255826]): failed to find FacadeSelector/Container child Transform
[10:14:16.730] [1] [INFO] [PLib/UI/SlippyCheeze.ImprovedFacadeSelection] Object Dump:
GameObject[FacadeSelector, 2 child(ren), Layer 5, Active=True]
    Translation=(500.41, 166.25, 0.00) [(-125.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
    Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=False, m_MinWidth=-1, m_MinHeight=-1, m_PreferredWidth=-1, m_PreferredHeight=-1, m_FlexibleWidth=-1, m_FlexibleHeight=-1, m_LayoutPriority=1]
    Component[UnityEngine.UI.VerticalLayoutGroup, Child Align=UpperLeft, Control W=True, Control H=True]
    Children:
        GameObject[Header, 2 child(ren), Layer 5, Active=True]
            Translation=(500.41, 166.25, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.00,1.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
            Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=False, m_MinWidth=250, m_MinHeight=24, m_PreferredWidth=-1, m_PreferredHeight=-1, m_FlexibleWidth=1, m_FlexibleHeight=-1, m_LayoutPriority=1]
            Children:
                GameObject[BG, 0 child(ren), Layer 5, Active=True]
                    Translation=(500.41, 166.25, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                    Component[ImageToggleState, TargetImage=BG (KImage), ActiveSprite=web_title (UnityEngine.Sprite), InactiveSprite=web_title (UnityEngine.Sprite), DisabledSprite=null, DisabledActiveSprite=null, useSprites=False, ActiveColour=RGBA(0.286, 0.286, 0.286, 1.000), InactiveColour=RGBA(0.286, 0.286, 0.286, 1.000), DisabledColour=RGBA(0.286, 0.286, 0.286, 1.000), DisabledActiveColour=RGBA(0.000, 0.000, 0.000, 0.000), HoverColour=RGBA(0.000, 0.000, 0.000, 0.000), DisabledHoverColor=RGBA(0.500, 0.500, 0.500, 1.000), colorStyleSetting=colorstyle_darkBG (ColorStyleSetting), isActive=False, currentState=Inactive, useStartingState=False, startingState=Inactive]
                    Component[KImage, Color=RGBA(0.286, 0.286, 0.286, 1.000), Sprite=web_title (UnityEngine.Sprite), defaultState=Active, colorSelector=Inactive, colorStyleSetting=null, clearMaskOnDisable=True]
                GameObject[Label, 0 child(ren), Layer 5, Active=True]
                    Translation=(508.73, 166.25, 0.00) [(5.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                    Rect[Size=(-10.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(10.00,0.00) OffsetMax=(0.00,0.00)]
                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                    Component[LocText, Text=_Select Facade:, Color=RGBA(1.000, 1.000, 1.000, 1.000), Font=Economica-Bold-OTF SDF (TMPro.TMP_FontAsset), key=STRINGS.UI.FACADE_SELECTION_PANEL.HEADER, textStyleSetting=style_titleTextSmall (TextStyleSetting), allowOverride=False, staticLayout=False, textLinkHandler=null, originalString=, allowLinksInternal=False]
                    Component[SetTextStyleSetting, text=null, sdfText=null, style=style_titleTextSmall (TextStyleSetting), currentStyle=null]
        GameObject[Scrollrect, 2 child(ren), Layer 5, Active=True]
            Translation=(500.41, 166.25, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
            Component[KScrollRect, currentSounds=[], scrollVelocity=0, default_intertia=True, default_elasticity=0.2, default_decelerationRate=0.02, verticalScrollInertiaScale=10, horizontalScrollInertiaScale=5, scrollDeceleration=0.25, forceContentMatchWidth=False, forceContentMatchHeight=False, allowHorizontalScrollWheel=True, allowVerticalScrollWheel=True, allowRightMouseScroll=False, scrollIsHorizontalOnly=False, panSpeed=20, mouseIsOver=False, panUp=False, panDown=False, panRight=False, panLeft=False, zoomInPan=False, zoomOutPan=False, keyboardScrollDelta=(0.00, 0.00, 0.00), keyboardScrollSpeed=1, startDrag=False, stopDrag=False, <isDragging>k__BackingField=False, autoScrolling=False, autoScrollTargetVerticalPos=0]
            Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=False, m_MinWidth=-1, m_MinHeight=116, m_PreferredWidth=-1, m_PreferredHeight=116, m_FlexibleWidth=-1, m_FlexibleHeight=-1, m_LayoutPriority=1]
            Component[UnityEngine.UI.Mask, m_RectTransform=null, m_ShowMaskGraphic=False, m_Graphic=null, m_MaskMaterial=null, m_UnmaskMaterial=null]
            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=web_box (UnityEngine.Sprite), m_Sprite=web_box (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
            Children:
                GameObject[Content, 2 child(ren), Layer 5, Active=True]
                    Translation=(500.41, 166.25, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.00,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                    Component[UnityEngine.UI.GridLayoutGroup, m_StartCorner=UpperLeft, m_StartAxis=Horizontal, m_CellSize=(48.00, 50.00), m_Spacing=(4.00, 4.00), m_Constraint=FixedColumnCount, m_ConstraintCount=5]
                    Component[UnityEngine.UI.ContentSizeFitter, m_HorizontalFit=PreferredSize, m_VerticalFit=PreferredSize, m_Rect=null, m_Tracker=UnityEngine.DrivenRectTransformTracker]
                    Children:
                        GameObject[FacadeTogglePrefab, 4 child(ren), Layer 5, Active=False]
                            Translation=(546.96, 118.04, 0.00) [(28.00, -29.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(48.00,50.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-54.00) OffsetMax=(52.00,-4.00)]
                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                            Component[MultiToggle, states=[ToggleState,ToggleState], play_sound_on_click=True, play_sound_on_release=False, toggle_image=BGImage (UnityEngine.UI.Image), state=0, onClick=null, stateDirty=True, onDoubleClick=null, onEnter=null, onExit=null, onHold=null, onStopHold=null, allowRightClick=True, clickHeldDown=False, totalHeldTime=0, heldTimeThreshold=0.4, pointerOver=False]
                            Component[HierarchyReferences, references=[ElementReference,ElementReference,ElementReference]]
                            Component[ToolTip, UseFixedStringKey=False, FixedStringKey=, multiStringToolTips=[], styleSettings=[], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(0.00, 0.00), tooltipPositionOffset=(8.00, 0.00), parentPositionAnchor=(1.00, 0.00), overrideParentObject=FacadeSelectionPanel(Clone) (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                            Children:
                                GameObject[BGImage, 0 child(ren), Layer 5, Active=False]
                                    Translation=(546.96, 118.04, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(48.00,50.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=web_button (UnityEngine.Sprite), m_Sprite=web_button (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[FGImage, 0 child(ren), Layer 5, Active=False]
                                    Translation=(546.96, 118.04, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(48.00,50.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=codexIconBuildings (UnityEngine.Sprite), m_Sprite=codexIconBuildings (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[Mannequin, 0 child(ren), Layer 5, Active=False]
                                    Translation=(546.96, 118.04, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(44.00,46.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(2.00,2.00) OffsetMax=(-2.00,-2.00)]
                                    Component[UIMannequin, animController=null, spawn=null, shouldShowOutfitWithDefaultItems=True, personalityToUseForDefaultClothing=None]
                                GameObject[DlcBanner, 0 child(ren), Layer 5, Active=False]
                                    Translation=(546.96, 159.60, 0.00) [(0.00, 25.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(-1.00, 1.00, 1.00)
                                    Rect[Size=(48.00,48.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,-48.00) OffsetMax=(48.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=dlc_banner (UnityEngine.Sprite), m_Sprite=dlc_banner (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                        GameObject[GetMoreButton, 2 child(ren), Layer 5, Active=False]
                            Translation=(546.96, 118.04, 0.00) [(28.00, -29.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(48.00,50.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-54.00) OffsetMax=(52.00,-4.00)]
                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                            Component[ToolTip, UseFixedStringKey=True, FixedStringKey=STRINGS.UI.FACADE_SELECTION_PANEL.STORE_BUTTON_TOOLTIP, multiStringToolTips=[], styleSettings=[], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=BottomCenter, tooltipPivot=(0.00, 1.00), tooltipPositionOffset=(0.00, -25.00), parentPositionAnchor=(0.50, 0.50), overrideParentObject=null, SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                            Component[KButton, soundPlayer=ButtonSoundPlayer, bgImage=BGImage (KImage), fgImage=null, additionalKImages=[], onClick=null, onDoubleClick=null, onBtnClick=null, onPointerEnter=null, onPointerExit=null, onPointerDown=null, onPointerUp=null, interactable=True, mouseOver=False]
                            Children:
                                GameObject[BGImage, 0 child(ren), Layer 5, Active=False]
                                    Translation=(546.96, 118.04, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(48.00,50.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[KImage, Color=RGBA(0.529, 0.272, 0.401, 1.000), Sprite=web_button (UnityEngine.Sprite), defaultState=Inactive, colorSelector=Inactive, colorStyleSetting=colorstyle_buttonPink (ColorStyleSetting), clearMaskOnDisable=True]
                                GameObject[FGImage, 0 child(ren), Layer 5, Active=False]
                                    Translation=(546.96, 118.04, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(40.00,42.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(4.00,4.00) OffsetMax=(-4.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=klei_logo (UnityEngine.Sprite), m_Sprite=klei_logo (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                GameObject[Scrollbar, 2 child(ren), Layer 5, Active=True]
                    Translation=(488.78, 159.60, 0.00) [(-7.00, -4.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                    Rect[Size=(8.00,-8.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(1.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(-11.00,4.00) OffsetMax=(-3.00,-4.00)]
                    Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=True, m_MinWidth=-1, m_MinHeight=-1, m_PreferredWidth=-1, m_PreferredHeight=-1, m_FlexibleWidth=-1, m_FlexibleHeight=-1, m_LayoutPriority=1]
                    Component[UnityEngine.UI.Scrollbar, m_HandleRect=Handle (UnityEngine.RectTransform), m_Direction=BottomToTop, m_Value=0, m_Size=1, m_NumberOfSteps=0, m_OnValueChanged=UnityEngine.UI.Scrollbar+ScrollEvent UnityEngine.UI.Scrollbar+ScrollEvent, m_ContainerRect=null, m_Offset=(0.00, 0.00), m_Tracker=UnityEngine.DrivenRectTransformTracker, m_PointerDownRepeat=null, isPointerDownAndNotDragging=False, m_DelayedUpdateVisuals=False]
                    Children:
                        GameObject[BG, 0 child(ren), Layer 5, Active=True]
                            Translation=(488.78, 166.25, 0.00) [(0.00, 4.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(8.00,-8.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=build_menu_scrollbar_frame (UnityEngine.Sprite), m_Sprite=build_menu_scrollbar_frame (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                        GameObject[Handle, 0 child(ren), Layer 5, Active=True]
                            Translation=(482.13, 172.90, 0.00) [(-4.00, 8.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(-4.00,-4.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(2.00,2.00) OffsetMax=(-2.00,-2.00)]
                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                            Component[UnityEngine.UI.Image, Color=RGBA(0.631, 0.639, 0.682, 1.000), Sprite=build_menu_scrollbar_inner (UnityEngine.Sprite), m_Sprite=build_menu_scrollbar_inner (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
```


```
GameObject[FacadeSelectionPanel, 1 child(ren), Layer 5, Active=True]
    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
    Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=False, m_MinWidth=-1, m_MinHeight=-1, m_PreferredWidth=-1, m_PreferredHeight=-1, m_FlexibleWidth=-1, m_FlexibleHeight=-1, m_LayoutPriority=1]
    Component[UnityEngine.UI.VerticalLayoutGroup, Child Align=UpperLeft, Control W=True, Control H=True]
    Component[FacadeSelectionPanel, togglePrefab=FacadeTogglePrefab (UnityEngine.GameObject), toggleContainer=Content (UnityEngine.RectTransform), usesScrollRect=False, scrollRect=null, activeFacadeToggles=[], pooledFacadeToggles=[], getMoreButton=GetMoreToggle (KButton), showGetMoreButton=True, hideWhenEmpty=False, useDummyPlaceholder=True, gridLayout=Content (UnityEngine.UI.GridLayoutGroup), dummyGridPlaceholders=[DummyPlaceholder (UnityEngine.GameObject),DummyPlaceholder (1) (UnityEngine.GameObject),DummyPlaceholder (2) (UnityEngine.GameObject),DummyPlaceholder (3) (UnityEngine.GameObject)], OnFacadeSelectionChanged=null, selectedOutfitCategory=Clothing, selectedBuildingDefID=null, currentConfigType=BuildingFacade, _selectedFacade=null]
    Component[SlippyCheeze.ImprovedFacadeSelection.ImprovedFacadeSelectionPanel, facadeSelectionPanel=FacadeSelectionPanel (FacadeSelectionPanel), rememberChoice=RememberChoice (UnityEngine.GameObject)]
    Children:
        GameObject[FacadeSelector, 3 child(ren), Layer 5, Active=True]
            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
            Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=False, m_MinWidth=-1, m_MinHeight=-1, m_PreferredWidth=-1, m_PreferredHeight=-1, m_FlexibleWidth=-1, m_FlexibleHeight=-1, m_LayoutPriority=1]
            Component[UnityEngine.UI.VerticalLayoutGroup, Child Align=UpperLeft, Control W=True, Control H=True]
            Children:
                GameObject[Header, 2 child(ren), Layer 5, Active=True]
                    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.00,1.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                    Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=False, m_MinWidth=250, m_MinHeight=24, m_PreferredWidth=-1, m_PreferredHeight=-1, m_FlexibleWidth=1, m_FlexibleHeight=-1, m_LayoutPriority=1]
                    Children:
                        GameObject[BG, 0 child(ren), Layer 5, Active=True]
                            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                            Component[ImageToggleState, TargetImage=BG (KImage), ActiveSprite=web_title (UnityEngine.Sprite), InactiveSprite=web_title (UnityEngine.Sprite), DisabledSprite=null, DisabledActiveSprite=null, useSprites=False, ActiveColour=RGBA(0.286, 0.286, 0.286, 1.000), InactiveColour=RGBA(0.286, 0.286, 0.286, 1.000), DisabledColour=RGBA(0.286, 0.286, 0.286, 1.000), DisabledActiveColour=RGBA(0.000, 0.000, 0.000, 0.000), HoverColour=RGBA(0.000, 0.000, 0.000, 0.000), DisabledHoverColor=RGBA(0.500, 0.500, 0.500, 1.000), colorStyleSetting=colorstyle_darkBG (ColorStyleSetting), isActive=False, currentState=Inactive, useStartingState=False, startingState=Inactive]
                            Component[KImage, Color=RGBA(0.286, 0.286, 0.286, 1.000), Sprite=web_title (UnityEngine.Sprite), defaultState=Active, colorSelector=Inactive, colorStyleSetting=null, clearMaskOnDisable=True]
                        GameObject[Label, 0 child(ren), Layer 5, Active=True]
                            Translation=(3238.87, -475.48, 0.00) [(5.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(-10.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(10.00,0.00) OffsetMax=(0.00,0.00)]
                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                            Component[LocText, Text=_Select Facade:, Color=RGBA(1.000, 1.000, 1.000, 1.000), Font=Economica-Bold-OTF SDF (TMPro.TMP_FontAsset), key=STRINGS.UI.FACADE_SELECTION_PANEL.HEADER, textStyleSetting=style_titleTextSmall (TextStyleSetting), allowOverride=False, staticLayout=False, textLinkHandler=null, originalString=, allowLinksInternal=False]
                            Component[SetTextStyleSetting, text=null, sdfText=null, style=style_titleTextSmall (TextStyleSetting), currentStyle=null]
                GameObject[RememberChoice, 1 child(ren), Layer 5, Active=True]
                    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                    Rect[Size=(119.77,20.00) Min=(119.77,20.00) Preferred=(119.77,20.00) Flexible=(1.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(-59.89,-10.00) OffsetMax=(59.89,10.00)]
                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                    Component[MultiToggle, states=[ToggleState,ToggleState,ToggleState], play_sound_on_click=True, play_sound_on_release=False, toggle_image=CheckMark (UnityEngine.UI.Image), state=0, onClick=System.Action, stateDirty=False, onDoubleClick=null, onEnter=null, onExit=null, onHold=null, onStopHold=null, allowRightClick=True, clickHeldDown=False, totalHeldTime=0, heldTimeThreshold=0.4, pointerOver=False]
                    Component[ToolTip, UseFixedStringKey=False, FixedStringKey=, multiStringToolTips=[PLACEHOLDER], styleSettings=[style_tooltip_description (TextStyleSetting)], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=BottomCenter, tooltipPivot=(0.50, 1.00), tooltipPositionOffset=(0.00, -25.00), parentPositionAnchor=(0.50, 0.50), overrideParentObject=null, SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                    Component[PeterHan.PLib.UI.RelativeLayoutGroup, locConstraints=[[AlignmentWrapper (UnityEngine.GameObject), PeterHan.PLib.UI.Layouts.RelativeLayoutParams]], serialConstraints=null, margin=null, results=[component=AlignmentWrapper 119.77x20.00]]
                    Children:
                        GameObject[AlignmentWrapper, 2 child(ren), Layer 5, Active=True]
                            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(119.77,20.00) Min=(119.77,20.00) Preferred=(119.77,20.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.50) AnchorMax=(0.00,0.50) OffsetMin=(0.00,-10.00) OffsetMax=(119.77,10.00)]
                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                            Component[PeterHan.PLib.UI.RelativeLayoutGroup, locConstraints=[[CheckBox (UnityEngine.GameObject), PeterHan.PLib.UI.Layouts.RelativeLayoutParams],[Text (UnityEngine.GameObject), PeterHan.PLib.UI.Layouts.RelativeLayoutParams]], serialConstraints=null, margin=null, results=[component=Text 96.77x19.04,component=CheckBox 20.00x20.00]]
                            Children:
                                GameObject[Text, 0 child(ren), Layer 5, Active=True]
                                    Translation=(3249.68, -475.48, 0.00) [(11.50, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(96.77,19.04) Min=(0.00,0.00) Preferred=(96.77,19.04) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.50) AnchorMax=(0.00,0.50) OffsetMin=(23.00,-9.52) OffsetMax=(119.77,9.52)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[LocText, Text=PLACEHOLDER, Color=RGBA(0.000, 0.000, 0.000, 1.000), Font=NotoSans-Regular (TMPro.TMP_FontAsset), key=, textStyleSetting= (TextStyleSetting), allowOverride=False, staticLayout=False, textLinkHandler=null, originalString=, allowLinksInternal=False]
                                    Component[SetTextStyleSetting, text=null, sdfText=Text (LocText), style= (TextStyleSetting), currentStyle= (TextStyleSetting)]
                                GameObject[CheckBox, 2 child(ren), Layer 5, Active=True]
                                    Translation=(3147.63, -475.48, 0.00) [(-49.89, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(20.00,20.00) Min=(20.00,20.00) Preferred=(20.00,20.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.50) AnchorMax=(0.00,0.50) OffsetMin=(0.00,-10.00) OffsetMax=(20.00,10.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), m_Sprite=null, m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                    Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=False, m_MinWidth=20, m_MinHeight=20, m_PreferredWidth=20, m_PreferredHeight=20, m_FlexibleWidth=0, m_FlexibleHeight=0, m_LayoutPriority=1]
                                    Children:
                                        GameObject[CheckBorder, 0 child(ren), Layer 5, Active=True]
                                            Translation=(3147.63, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(20.00,20.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.000, 0.000, 0.000, 1.000), Sprite=overview_jobs_skill_box (UnityEngine.Sprite), m_Sprite=overview_jobs_skill_box (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[CheckMark, 0 child(ren), Layer 5, Active=True]
                                            Translation=(3147.63, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(16.00,16.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.50,0.50) AnchorMax=(0.50,0.50) OffsetMin=(-8.00,-8.00) OffsetMax=(8.00,8.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 0.000), m_Sprite=null, m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                GameObject[Container, 1 child(ren), Layer 5, Active=True]
                    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                    Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=web_box (UnityEngine.Sprite), m_Sprite=web_box (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                    Component[UnityEngine.UI.VerticalLayoutGroup, Child Align=UpperLeft, Control W=True, Control H=True]
                    Children:
                        GameObject[Content, 6 child(ren), Layer 5, Active=True]
                            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(250.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.00,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(250.00,0.00)]
                            Component[UnityEngine.UI.GridLayoutGroup, m_StartCorner=UpperLeft, m_StartAxis=Horizontal, m_CellSize=(78.00, 78.00), m_Spacing=(4.00, 4.00), m_Constraint=FixedColumnCount, m_ConstraintCount=3]
                            Component[UnityEngine.UI.ContentSizeFitter, m_HorizontalFit=PreferredSize, m_VerticalFit=PreferredSize, m_Rect=null, m_Tracker=UnityEngine.DrivenRectTransformTracker]
                            Children:
                                GameObject[FacadeTogglePrefab, 4 child(ren), Layer 5, Active=False]
                                    Translation=(3302.05, -546.96, 0.00) [(43.00, -43.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-82.00) OffsetMax=(82.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[MultiToggle, states=[ToggleState,ToggleState], play_sound_on_click=True, play_sound_on_release=False, toggle_image=BGImage (UnityEngine.UI.Image), state=0, onClick=null, stateDirty=True, onDoubleClick=null, onEnter=null, onExit=null, onHold=null, onStopHold=null, allowRightClick=True, clickHeldDown=False, totalHeldTime=0, heldTimeThreshold=0.4, pointerOver=False]
                                    Component[HierarchyReferences, references=[ElementReference,ElementReference,ElementReference]]
                                    Component[ToolTip, UseFixedStringKey=False, FixedStringKey=, multiStringToolTips=[], styleSettings=[], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=False]
                                            Translation=(3302.05, -546.96, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=web_button (UnityEngine.Sprite), m_Sprite=web_button (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=False]
                                            Translation=(3302.05, -546.96, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=codexIconBuildings (UnityEngine.Sprite), m_Sprite=codexIconBuildings (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[Mannequin, 0 child(ren), Layer 5, Active=False]
                                            Translation=(3302.05, -546.96, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UIMannequin, animController=null, spawn=null, shouldShowOutfitWithDefaultItems=True, personalityToUseForDefaultClothing=None]
                                        GameObject[DlcBanner, 0 child(ren), Layer 5, Active=False]
                                            Translation=(3290.41, -482.13, 0.00) [(-7.00, 39.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(-1.00, 1.00, 1.00)
                                            Rect[Size=(64.00,64.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,-64.00) OffsetMax=(64.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=dlc_banner (UnityEngine.Sprite), m_Sprite=dlc_banner (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[GetMoreToggle, 2 child(ren), Layer 5, Active=False]
                                    Translation=(3277.11, -523.69, 0.00) [(28.00, -29.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(48.00,50.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-54.00) OffsetMax=(52.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[ToolTip, UseFixedStringKey=True, FixedStringKey=STRINGS.UI.FACADE_SELECTION_PANEL.STORE_BUTTON_TOOLTIP, multiStringToolTips=[], styleSettings=[], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Component[KButton, soundPlayer=ButtonSoundPlayer, bgImage=BGImage (KImage), fgImage=null, additionalKImages=[], onClick=null, onDoubleClick=null, onBtnClick=null, onPointerEnter=null, onPointerExit=null, onPointerDown=null, onPointerUp=null, interactable=True, mouseOver=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=False]
                                            Translation=(3277.11, -523.69, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(48.00,50.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[KImage, Color=RGBA(0.529, 0.272, 0.401, 1.000), Sprite=web_button (UnityEngine.Sprite), defaultState=Inactive, colorSelector=Inactive, colorStyleSetting=colorstyle_button (ColorStyleSetting), clearMaskOnDisable=True]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=False]
                                            Translation=(3277.11, -523.69, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(44.00,46.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(2.00,2.00) OffsetMax=(-2.00,-2.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=icon_display_screen_blueprint (UnityEngine.Sprite), m_Sprite=icon_display_screen_blueprint (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[DummyPlaceholder, 1 child(ren), Layer 5, Active=True]
                                    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.341, 0.341, 0.341, 0.322), Sprite=web_border (UnityEngine.Sprite), m_Sprite=web_border (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[DummyPlaceholder (1), 1 child(ren), Layer 5, Active=True]
                                    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.341, 0.341, 0.341, 0.322), Sprite=web_border (UnityEngine.Sprite), m_Sprite=web_border (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[DummyPlaceholder (2), 1 child(ren), Layer 5, Active=True]
                                    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.341, 0.341, 0.341, 0.322), Sprite=web_border (UnityEngine.Sprite), m_Sprite=web_border (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[DummyPlaceholder (3), 1 child(ren), Layer 5, Active=True]
                                    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.341, 0.341, 0.341, 0.322), Sprite=web_border (UnityEngine.Sprite), m_Sprite=web_border (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
17:53:55.9386 M1  SC:I ImprovedFacadeSelectionPanel.OnRememberChoiceClicked] ImprovedFacadeSelectionPanel(FacadeSelectionPanel[-556526]): OnRememberChoiceClicked(RememberChoice[-666596], 0)
17:53:56.0935 M1  SC:I ImprovedFacadeSelectionPanel.OnRememberChoiceClicked] ImprovedFacadeSelectionPanel(FacadeSelectionPanel[-556526]): OnRememberChoiceClicked(RememberChoice[-666596], 0)
17:53:56.2530 M1  SC:I ImprovedFacadeSelectionPanel.OnRememberChoiceClicked] ImprovedFacadeSelectionPanel(FacadeSelectionPanel[-556526]): OnRememberChoiceClicked(RememberChoice[-666596], 0)
17:53:56.4271 M1  SC:I ImprovedFacadeSelectionPanel.OnRememberChoiceClicked] ImprovedFacadeSelectionPanel(FacadeSelectionPanel[-556526]): OnRememberChoiceClicked(RememberChoice[-666596], 0)
17:53:56.6162 M1  SC:I ImprovedFacadeSelectionPanel.OnRememberChoiceClicked] ImprovedFacadeSelectionPanel(FacadeSelectionPanel[-556526]): OnRememberChoiceClicked(RememberChoice[-666596], 0)
17:53:56.7889 M1  SC:I ImprovedFacadeSelectionPanel.OnRememberChoiceClicked] ImprovedFacadeSelectionPanel(FacadeSelectionPanel[-556526]): OnRememberChoiceClicked(RememberChoice[-666596], 0)
```


# WHEN A BUILDINGDEF HAS BEEN SET

```
GameObject[FacadeSelectionPanel, 1 child(ren), Layer 5, Active=True]
    Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
    Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=False, m_MinWidth=-1, m_MinHeight=-1, m_PreferredWidth=-1, m_PreferredHeight=-1, m_FlexibleWidth=-1, m_FlexibleHeight=-1, m_LayoutPriority=1]
    Component[UnityEngine.UI.VerticalLayoutGroup, Child Align=UpperLeft, Control W=True, Control H=True]
    Component[FacadeSelectionPanel, togglePrefab=FacadeTogglePrefab (UnityEngine.GameObject), toggleContainer=Content (UnityEngine.RectTransform), usesScrollRect=False, scrollRect=null, activeFacadeToggles=[[DEFAULT_FACADE, FacadeSelectionPanel+FacadeToggle],[WaterCooler_round_body, FacadeSelectionPanel+FacadeToggle],[WaterCooler_balloon, FacadeSelectionPanel+FacadeToggle],[WaterCooler_yellow_tartar, FacadeSelectionPanel+FacadeToggle],[WaterCooler_red_rose, FacadeSelectionPanel+FacadeToggle],[WaterCooler_green_mush, FacadeSelectionPanel+FacadeToggle],[WaterCooler_purple_brainfat, FacadeSelectionPanel+FacadeToggle],[WaterCooler_blue_babytears, FacadeSelectionPanel+FacadeToggle]], pooledFacadeToggles=[], getMoreButton=GetMoreToggle (KButton), showGetMoreButton=True, hideWhenEmpty=False, useDummyPlaceholder=True, gridLayout=Content (UnityEngine.UI.GridLayoutGroup), dummyGridPlaceholders=[DummyPlaceholder (UnityEngine.GameObject),DummyPlaceholder (1) (UnityEngine.GameObject),DummyPlaceholder (2) (UnityEngine.GameObject),DummyPlaceholder (3) (UnityEngine.GameObject)], OnFacadeSelectionChanged=null, selectedOutfitCategory=Clothing, selectedBuildingDefID=WaterCooler, currentConfigType=BuildingFacade, _selectedFacade=DEFAULT_FACADE]
    Children:
        GameObject[FacadeSelector, 2 child(ren), Layer 5, Active=True]
            Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
            Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=False, m_MinWidth=-1, m_MinHeight=-1, m_PreferredWidth=-1, m_PreferredHeight=-1, m_FlexibleWidth=-1, m_FlexibleHeight=-1, m_LayoutPriority=1]
            Component[UnityEngine.UI.VerticalLayoutGroup, Child Align=UpperLeft, Control W=True, Control H=True]
            Children:
                GameObject[Header, 2 child(ren), Layer 5, Active=True]
                    Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                    Rect[Size=(0.00,0.00) Min=(250.00,24.00) Preferred=(250.00,24.00) Flexible=(1.00,0.00) Pivot=(0.00,1.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                    Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=False, m_MinWidth=250, m_MinHeight=24, m_PreferredWidth=-1, m_PreferredHeight=-1, m_FlexibleWidth=1, m_FlexibleHeight=-1, m_LayoutPriority=1]
                    Children:
                        GameObject[BG, 0 child(ren), Layer 5, Active=True]
                            Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(16.00,16.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                            Component[ImageToggleState, TargetImage=BG (KImage), ActiveSprite=web_title (UnityEngine.Sprite), InactiveSprite=web_title (UnityEngine.Sprite), DisabledSprite=null, DisabledActiveSprite=null, useSprites=False, ActiveColour=RGBA(0.286, 0.286, 0.286, 1.000), InactiveColour=RGBA(0.286, 0.286, 0.286, 1.000), DisabledColour=RGBA(0.286, 0.286, 0.286, 1.000), DisabledActiveColour=RGBA(0.000, 0.000, 0.000, 0.000), HoverColour=RGBA(0.000, 0.000, 0.000, 0.000), DisabledHoverColor=RGBA(0.500, 0.500, 0.500, 1.000), colorStyleSetting=colorstyle_darkBG (ColorStyleSetting), isActive=False, currentState=Inactive, useStartingState=False, startingState=Inactive]
                            Component[KImage, Color=RGBA(0.286, 0.286, 0.286, 1.000), Sprite=web_title (UnityEngine.Sprite), defaultState=Active, colorSelector=Active, colorStyleSetting=null, clearMaskOnDisable=True]
                        GameObject[Label, 0 child(ren), Layer 5, Active=True]
                            Translation=(2849.32, 172.90, 0.00) [(5.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(-10.00,0.00) Min=(0.00,0.00) Preferred=(84.43,275.72) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(10.00,0.00) OffsetMax=(0.00,0.00)]
                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                            Component[LocText, Text=Select Blueprint, Color=RGBA(1.000, 1.000, 1.000, 1.000), Font=Economica-Bold-OTF SDF (TMPro.TMP_FontAsset), key=STRINGS.UI.FACADE_SELECTION_PANEL.HEADER, textStyleSetting=style_titleTextSmall (TextStyleSetting), allowOverride=False, staticLayout=False, textLinkHandler=null, originalString=, allowLinksInternal=False]
                            Component[SetTextStyleSetting, text=null, sdfText=Label (LocText), style=style_titleTextSmall (TextStyleSetting), currentStyle=style_titleTextSmall (TextStyleSetting)]
                GameObject[Container, 1 child(ren), Layer 5, Active=True]
                    Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(4.00,4.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                    Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=web_box (UnityEngine.Sprite), m_Sprite=web_box (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                    Component[UnityEngine.UI.VerticalLayoutGroup, Child Align=UpperLeft, Control W=True, Control H=True]
                    Children:
                        GameObject[Content, 14 child(ren), Layer 5, Active=True]
                            Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(250.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.00,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(250.00,0.00)]
                            Component[UnityEngine.UI.GridLayoutGroup, m_StartCorner=UpperLeft, m_StartAxis=Horizontal, m_CellSize=(78.00, 78.00), m_Spacing=(4.00, 4.00), m_Constraint=FixedColumnCount, m_ConstraintCount=3]
                            Component[UnityEngine.UI.ContentSizeFitter, m_HorizontalFit=PreferredSize, m_VerticalFit=PreferredSize, m_Rect=Content (UnityEngine.RectTransform), m_Tracker=UnityEngine.DrivenRectTransformTracker]
                            Children:
                                GameObject[FacadeTogglePrefab, 4 child(ren), Layer 5, Active=True]
                                    Translation=(2912.49, 101.41, 0.00) [(43.00, -43.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-82.00) OffsetMax=(82.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[MultiToggle, states=[ToggleState,ToggleState], play_sound_on_click=True, play_sound_on_release=False, toggle_image=BGImage (UnityEngine.UI.Image), state=1, onClick=System.Action, stateDirty=False, onDoubleClick=null, onEnter=null, onExit=null, onHold=null, onStopHold=null, allowRightClick=True, clickHeldDown=False, totalHeldTime=0, heldTimeThreshold=0.4, pointerOver=False]
                                    Component[HierarchyReferences, references=[ElementReference,ElementReference,ElementReference]]
                                    Component[ToolTip, UseFixedStringKey=False, FixedStringKey=, multiStringToolTips=[<b><style="KLink"><link="WATERCOOLER">Water Cooler</link></style></b>
Where Duplicants sip and socialize.], styleSettings=[style_tooltip_description (TextStyleSetting)], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(10.00,10.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.502, 0.545, 0.698, 1.000), Sprite=web_button (UnityEngine.Sprite), m_Sprite=web_button (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(264.00,268.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=watercooler_0:ui:False (UnityEngine.Sprite), m_Sprite=watercooler_0:ui:False (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[Mannequin, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UIMannequin, animController=null, spawn=null, shouldShowOutfitWithDefaultItems=True, personalityToUseForDefaultClothing=None]
                                        GameObject[DlcBanner, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2900.85, 166.25, 0.00) [(-7.00, 39.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(-1.00, 1.00, 1.00)
                                            Rect[Size=(64.00,64.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,-64.00) OffsetMax=(64.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=dlc_banner (UnityEngine.Sprite), m_Sprite=dlc_banner (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[FacadeTogglePrefab, 4 child(ren), Layer 5, Active=False]
                                    Translation=(2912.49, 101.41, 0.00) [(43.00, -43.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-82.00) OffsetMax=(82.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[MultiToggle, states=[ToggleState,ToggleState], play_sound_on_click=True, play_sound_on_release=False, toggle_image=BGImage (UnityEngine.UI.Image), state=0, onClick=null, stateDirty=True, onDoubleClick=null, onEnter=null, onExit=null, onHold=null, onStopHold=null, allowRightClick=True, clickHeldDown=False, totalHeldTime=0, heldTimeThreshold=0.4, pointerOver=False]
                                    Component[HierarchyReferences, references=[ElementReference,ElementReference,ElementReference]]
                                    Component[ToolTip, UseFixedStringKey=False, FixedStringKey=, multiStringToolTips=[], styleSettings=[], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=web_button (UnityEngine.Sprite), m_Sprite=web_button (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=codexIconBuildings (UnityEngine.Sprite), m_Sprite=codexIconBuildings (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[Mannequin, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UIMannequin, animController=null, spawn=null, shouldShowOutfitWithDefaultItems=True, personalityToUseForDefaultClothing=None]
                                        GameObject[DlcBanner, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2900.85, 166.25, 0.00) [(-7.00, 39.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(-1.00, 1.00, 1.00)
                                            Rect[Size=(64.00,64.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,-64.00) OffsetMax=(64.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=dlc_banner (UnityEngine.Sprite), m_Sprite=dlc_banner (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[GetMoreToggle, 2 child(ren), Layer 5, Active=False]
                                    Translation=(2887.55, 124.69, 0.00) [(28.00, -29.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(48.00,50.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-54.00) OffsetMax=(52.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[ToolTip, UseFixedStringKey=True, FixedStringKey=STRINGS.UI.FACADE_SELECTION_PANEL.STORE_BUTTON_TOOLTIP, multiStringToolTips=[], styleSettings=[], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Component[KButton, soundPlayer=ButtonSoundPlayer, bgImage=BGImage (KImage), fgImage=null, additionalKImages=[], onClick=null, onDoubleClick=null, onBtnClick=null, onPointerEnter=null, onPointerExit=null, onPointerDown=null, onPointerUp=null, interactable=True, mouseOver=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2887.55, 124.69, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(48.00,50.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[KImage, Color=RGBA(0.529, 0.272, 0.401, 1.000), Sprite=web_button (UnityEngine.Sprite), defaultState=Inactive, colorSelector=Inactive, colorStyleSetting=colorstyle_button (ColorStyleSetting), clearMaskOnDisable=True]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2887.55, 124.69, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(44.00,46.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(2.00,2.00) OffsetMax=(-2.00,-2.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=icon_display_screen_blueprint (UnityEngine.Sprite), m_Sprite=icon_display_screen_blueprint (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[DummyPlaceholder, 1 child(ren), Layer 5, Active=True]
                                    Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(6.00,6.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.341, 0.341, 0.341, 0.322), Sprite=web_border (UnityEngine.Sprite), m_Sprite=web_border (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[DummyPlaceholder (1), 1 child(ren), Layer 5, Active=True]
                                    Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(6.00,6.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.341, 0.341, 0.341, 0.322), Sprite=web_border (UnityEngine.Sprite), m_Sprite=web_border (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[DummyPlaceholder (2), 1 child(ren), Layer 5, Active=True]
                                    Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(6.00,6.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.341, 0.341, 0.341, 0.322), Sprite=web_border (UnityEngine.Sprite), m_Sprite=web_border (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[DummyPlaceholder (3), 1 child(ren), Layer 5, Active=True]
                                    Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2841.00, 172.90, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(6.00,6.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.341, 0.341, 0.341, 0.322), Sprite=web_border (UnityEngine.Sprite), m_Sprite=web_border (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[FacadeTogglePrefab, 4 child(ren), Layer 5, Active=True]
                                    Translation=(2912.49, 101.41, 0.00) [(43.00, -43.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-82.00) OffsetMax=(82.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[MultiToggle, states=[ToggleState,ToggleState], play_sound_on_click=True, play_sound_on_release=False, toggle_image=BGImage (UnityEngine.UI.Image), state=0, onClick=System.Action, stateDirty=False, onDoubleClick=null, onEnter=null, onExit=null, onHold=null, onStopHold=null, allowRightClick=True, clickHeldDown=False, totalHeldTime=0, heldTimeThreshold=0.4, pointerOver=False]
                                    Component[HierarchyReferences, references=[ElementReference,ElementReference,ElementReference]]
                                    Component[ToolTip, UseFixedStringKey=False, FixedStringKey=, multiStringToolTips=[<b><style="KLink"><link="WATERCOOLER">Elegant Water Cooler</link></style></b>
It really classes up a breakroom.

<color=#FF6DE7>Splendid</color> quality.], styleSettings=[style_tooltip_description (TextStyleSetting)], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(10.00,10.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.243, 0.263, 0.341, 1.000), Sprite=web_button (UnityEngine.Sprite), m_Sprite=web_button (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(254.00,266.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=watercooler_round_body_0:ui:False (UnityEngine.Sprite), m_Sprite=watercooler_round_body_0:ui:False (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[Mannequin, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UIMannequin, animController=null, spawn=null, shouldShowOutfitWithDefaultItems=True, personalityToUseForDefaultClothing=None]
                                        GameObject[DlcBanner, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2900.85, 166.25, 0.00) [(-7.00, 39.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(-1.00, 1.00, 1.00)
                                            Rect[Size=(64.00,64.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,-64.00) OffsetMax=(64.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=dlc_banner (UnityEngine.Sprite), m_Sprite=dlc_banner (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[FacadeTogglePrefab, 4 child(ren), Layer 5, Active=True]
                                    Translation=(2912.49, 101.41, 0.00) [(43.00, -43.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-82.00) OffsetMax=(82.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[MultiToggle, states=[ToggleState,ToggleState], play_sound_on_click=True, play_sound_on_release=False, toggle_image=BGImage (UnityEngine.UI.Image), state=0, onClick=System.Action, stateDirty=False, onDoubleClick=null, onEnter=null, onExit=null, onHold=null, onStopHold=null, allowRightClick=True, clickHeldDown=False, totalHeldTime=0, heldTimeThreshold=0.4, pointerOver=False]
                                    Component[HierarchyReferences, references=[ElementReference,ElementReference,ElementReference]]
                                    Component[ToolTip, UseFixedStringKey=False, FixedStringKey=, multiStringToolTips=[<b><style="KLink"><link="WATERCOOLER">Inflatable Water Cooler</link></style></b>
There's a funny aftertaste.

<color=#FF6DE7>Splendid</color> quality.

<color=#DD992FFF>My colony doesn't have any of these yet.</color>], styleSettings=[style_tooltip_description (TextStyleSetting)], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(10.00,10.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.243, 0.263, 0.341, 1.000), Sprite=web_button (UnityEngine.Sprite), m_Sprite=web_button (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(250.00,242.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=watercooler_balloon_0:ui:False (UnityEngine.Sprite), m_Sprite=watercooler_balloon_0:ui:False (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[Mannequin, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UIMannequin, animController=null, spawn=null, shouldShowOutfitWithDefaultItems=True, personalityToUseForDefaultClothing=None]
                                        GameObject[DlcBanner, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2900.85, 166.25, 0.00) [(-7.00, 39.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(-1.00, 1.00, 1.00)
                                            Rect[Size=(64.00,64.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,-64.00) OffsetMax=(64.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=dlc_banner (UnityEngine.Sprite), m_Sprite=dlc_banner (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[FacadeTogglePrefab, 4 child(ren), Layer 5, Active=True]
                                    Translation=(2912.49, 101.41, 0.00) [(43.00, -43.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-82.00) OffsetMax=(82.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[MultiToggle, states=[ToggleState,ToggleState], play_sound_on_click=True, play_sound_on_release=False, toggle_image=BGImage (UnityEngine.UI.Image), state=0, onClick=System.Action, stateDirty=False, onDoubleClick=null, onEnter=null, onExit=null, onHold=null, onStopHold=null, allowRightClick=True, clickHeldDown=False, totalHeldTime=0, heldTimeThreshold=0.4, pointerOver=False]
                                    Component[HierarchyReferences, references=[ElementReference,ElementReference,ElementReference]]
                                    Component[ToolTip, UseFixedStringKey=False, FixedStringKey=, multiStringToolTips=[<b><style="KLink"><link="WATERCOOLER">Ick Yellow Water Cooler</link></style></b>
Did someone boil eggs in this water?

<color=#71E379>Nifty</color> quality.

<color=#DD992FFF>My colony doesn't have any of these yet.</color>], styleSettings=[style_tooltip_description (TextStyleSetting)], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(10.00,10.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.243, 0.263, 0.341, 1.000), Sprite=web_button (UnityEngine.Sprite), m_Sprite=web_button (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(251.00,257.02) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=watercooler_yellow_tartar_0:ui:False (UnityEngine.Sprite), m_Sprite=watercooler_yellow_tartar_0:ui:False (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[Mannequin, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UIMannequin, animController=null, spawn=null, shouldShowOutfitWithDefaultItems=True, personalityToUseForDefaultClothing=None]
                                        GameObject[DlcBanner, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2900.85, 166.25, 0.00) [(-7.00, 39.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(-1.00, 1.00, 1.00)
                                            Rect[Size=(64.00,64.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,-64.00) OffsetMax=(64.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=dlc_banner (UnityEngine.Sprite), m_Sprite=dlc_banner (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[FacadeTogglePrefab, 4 child(ren), Layer 5, Active=True]
                                    Translation=(2912.49, 101.41, 0.00) [(43.00, -43.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-82.00) OffsetMax=(82.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[MultiToggle, states=[ToggleState,ToggleState], play_sound_on_click=True, play_sound_on_release=False, toggle_image=BGImage (UnityEngine.UI.Image), state=0, onClick=System.Action, stateDirty=False, onDoubleClick=null, onEnter=null, onExit=null, onHold=null, onStopHold=null, allowRightClick=True, clickHeldDown=False, totalHeldTime=0, heldTimeThreshold=0.4, pointerOver=False]
                                    Component[HierarchyReferences, references=[ElementReference,ElementReference,ElementReference]]
                                    Component[ToolTip, UseFixedStringKey=False, FixedStringKey=, multiStringToolTips=[<b><style="KLink"><link="WATERCOOLER">Puce Pink Water Cooler</link></style></b>
Rose-colored paper cups: the shatter-proof alternative to rose-colored glasses.

<color=#71E379>Nifty</color> quality.

<color=#DD992FFF>My colony doesn't have any of these yet.</color>], styleSettings=[style_tooltip_description (TextStyleSetting)], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(10.00,10.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.243, 0.263, 0.341, 1.000), Sprite=web_button (UnityEngine.Sprite), m_Sprite=web_button (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(251.00,257.02) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=watercooler_red_rose_0:ui:False (UnityEngine.Sprite), m_Sprite=watercooler_red_rose_0:ui:False (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[Mannequin, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UIMannequin, animController=null, spawn=null, shouldShowOutfitWithDefaultItems=True, personalityToUseForDefaultClothing=None]
                                        GameObject[DlcBanner, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2900.85, 166.25, 0.00) [(-7.00, 39.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(-1.00, 1.00, 1.00)
                                            Rect[Size=(64.00,64.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,-64.00) OffsetMax=(64.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=dlc_banner (UnityEngine.Sprite), m_Sprite=dlc_banner (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[FacadeTogglePrefab, 4 child(ren), Layer 5, Active=True]
                                    Translation=(2912.49, 101.41, 0.00) [(43.00, -43.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-82.00) OffsetMax=(82.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[MultiToggle, states=[ToggleState,ToggleState], play_sound_on_click=True, play_sound_on_release=False, toggle_image=BGImage (UnityEngine.UI.Image), state=0, onClick=System.Action, stateDirty=False, onDoubleClick=null, onEnter=null, onExit=null, onHold=null, onStopHold=null, allowRightClick=True, clickHeldDown=False, totalHeldTime=0, heldTimeThreshold=0.4, pointerOver=False]
                                    Component[HierarchyReferences, references=[ElementReference,ElementReference,ElementReference]]
                                    Component[ToolTip, UseFixedStringKey=False, FixedStringKey=, multiStringToolTips=[<b><style="KLink"><link="WATERCOOLER">Mush Green Water Cooler</link></style></b>
Ideal for post-Mush Bar palate cleansing.

<color=#71E379>Nifty</color> quality.

<color=#DD992FFF>My colony doesn't have any of these yet.</color>], styleSettings=[style_tooltip_description (TextStyleSetting)], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(10.00,10.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.243, 0.263, 0.341, 1.000), Sprite=web_button (UnityEngine.Sprite), m_Sprite=web_button (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(251.00,257.02) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=watercooler_green_mush_0:ui:False (UnityEngine.Sprite), m_Sprite=watercooler_green_mush_0:ui:False (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[Mannequin, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UIMannequin, animController=null, spawn=null, shouldShowOutfitWithDefaultItems=True, personalityToUseForDefaultClothing=None]
                                        GameObject[DlcBanner, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2900.85, 166.25, 0.00) [(-7.00, 39.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(-1.00, 1.00, 1.00)
                                            Rect[Size=(64.00,64.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,-64.00) OffsetMax=(64.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=dlc_banner (UnityEngine.Sprite), m_Sprite=dlc_banner (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[FacadeTogglePrefab, 4 child(ren), Layer 5, Active=True]
                                    Translation=(2912.49, 101.41, 0.00) [(43.00, -43.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-82.00) OffsetMax=(82.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[MultiToggle, states=[ToggleState,ToggleState], play_sound_on_click=True, play_sound_on_release=False, toggle_image=BGImage (UnityEngine.UI.Image), state=0, onClick=System.Action, stateDirty=False, onDoubleClick=null, onEnter=null, onExit=null, onHold=null, onStopHold=null, allowRightClick=True, clickHeldDown=False, totalHeldTime=0, heldTimeThreshold=0.4, pointerOver=False]
                                    Component[HierarchyReferences, references=[ElementReference,ElementReference,ElementReference]]
                                    Component[ToolTip, UseFixedStringKey=False, FixedStringKey=, multiStringToolTips=[<b><style="KLink"><link="WATERCOOLER">Faint Purple Water Cooler</link></style></b>
Most Duplicants agree that it really should dispense juice.

<color=#71E379>Nifty</color> quality.

<color=#DD992FFF>My colony doesn't have any of these yet.</color>], styleSettings=[style_tooltip_description (TextStyleSetting)], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(10.00,10.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.243, 0.263, 0.341, 1.000), Sprite=web_button (UnityEngine.Sprite), m_Sprite=web_button (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(251.00,257.02) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=watercooler_purple_brainfat_0:ui:False (UnityEngine.Sprite), m_Sprite=watercooler_purple_brainfat_0:ui:False (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[Mannequin, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UIMannequin, animController=null, spawn=null, shouldShowOutfitWithDefaultItems=True, personalityToUseForDefaultClothing=None]
                                        GameObject[DlcBanner, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2900.85, 166.25, 0.00) [(-7.00, 39.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(-1.00, 1.00, 1.00)
                                            Rect[Size=(64.00,64.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,-64.00) OffsetMax=(64.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=dlc_banner (UnityEngine.Sprite), m_Sprite=dlc_banner (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[FacadeTogglePrefab, 4 child(ren), Layer 5, Active=True]
                                    Translation=(2912.49, 101.41, 0.00) [(43.00, -43.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-82.00) OffsetMax=(82.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[MultiToggle, states=[ToggleState,ToggleState], play_sound_on_click=True, play_sound_on_release=False, toggle_image=BGImage (UnityEngine.UI.Image), state=0, onClick=System.Action, stateDirty=False, onDoubleClick=null, onEnter=null, onExit=null, onHold=null, onStopHold=null, allowRightClick=True, clickHeldDown=False, totalHeldTime=0, heldTimeThreshold=0.4, pointerOver=False]
                                    Component[HierarchyReferences, references=[ElementReference,ElementReference,ElementReference]]
                                    Component[ToolTip, UseFixedStringKey=False, FixedStringKey=, multiStringToolTips=[<b><style="KLink"><link="WATERCOOLER">Weepy Blue Water Cooler</link></style></b>
Lightly salted with Duplicants' tears.

<color=#71E379>Nifty</color> quality.

<color=#DD992FFF>My colony doesn't have any of these yet.</color>], styleSettings=[style_tooltip_description (TextStyleSetting)], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(10.00,10.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.243, 0.263, 0.341, 1.000), Sprite=web_button (UnityEngine.Sprite), m_Sprite=web_button (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(251.00,257.02) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=watercooler_blue_babytears_0:ui:False (UnityEngine.Sprite), m_Sprite=watercooler_blue_babytears_0:ui:False (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[Mannequin, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2912.49, 101.41, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UIMannequin, animController=null, spawn=null, shouldShowOutfitWithDefaultItems=True, personalityToUseForDefaultClothing=None]
                                        GameObject[DlcBanner, 0 child(ren), Layer 5, Active=False]
                                            Translation=(2900.85, 166.25, 0.00) [(-7.00, 39.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(-1.00, 1.00, 1.00)
                                            Rect[Size=(64.00,64.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,-64.00) OffsetMax=(64.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=dlc_banner (UnityEngine.Sprite), m_Sprite=dlc_banner (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
```


```
[03:49:17.228] [1] [INFO] [PLib/UI/SlippyCheeze.ModeratelyImprovedBlueprintSelection] Object Tree:
- FacadeSelectionPanel
- CosmeticSlotContainer
- CosmeticsPanel
- Content
- ScrollRect
- Horizontal
- CategoryHeader
- BodySkin
- Body
- SideScreen
- DetailsScreen
- RootMenuRight
- BottomPanelRight
- ScreenSpaceOverlayCanvas
- GameScreenManager
[03:49:17.234] [1] [INFO] [PLib/UI/SlippyCheeze.ModeratelyImprovedBlueprintSelection] Object Dump:
GameObject[FacadeSelectionPanel, 1 child(ren), Layer 5, Active=True]
    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
    Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=False, m_MinWidth=-1, m_MinHeight=-1, m_PreferredWidth=-1, m_PreferredHeight=-1, m_FlexibleWidth=-1, m_FlexibleHeight=-1, m_LayoutPriority=1]
    Component[UnityEngine.UI.VerticalLayoutGroup, Child Align=UpperLeft, Control W=True, Control H=True]
    Component[FacadeSelectionPanel, togglePrefab=FacadeTogglePrefab (UnityEngine.GameObject), toggleContainer=Content (UnityEngine.RectTransform), usesScrollRect=False, scrollRect=null, activeFacadeToggles=[], pooledFacadeToggles=[], getMoreButton=GetMoreToggle (KButton), showGetMoreButton=True, hideWhenEmpty=False, useDummyPlaceholder=True, gridLayout=Content (UnityEngine.UI.GridLayoutGroup), dummyGridPlaceholders=[DummyPlaceholder (UnityEngine.GameObject),DummyPlaceholder (1) (UnityEngine.GameObject),DummyPlaceholder (2) (UnityEngine.GameObject),DummyPlaceholder (3) (UnityEngine.GameObject)], OnFacadeSelectionChanged=null, selectedOutfitCategory=Clothing, selectedBuildingDefID=null, currentConfigType=BuildingFacade, _selectedFacade=null]
    Children:
        GameObject[FacadeSelector, 2 child(ren), Layer 5, Active=True]
            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
            Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=False, m_MinWidth=-1, m_MinHeight=-1, m_PreferredWidth=-1, m_PreferredHeight=-1, m_FlexibleWidth=-1, m_FlexibleHeight=-1, m_LayoutPriority=1]
            Component[UnityEngine.UI.VerticalLayoutGroup, Child Align=UpperLeft, Control W=True, Control H=True]
            Children:
                GameObject[Header, 2 child(ren), Layer 5, Active=True]
                    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.00,1.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                    Component[UnityEngine.UI.LayoutElement, m_IgnoreLayout=False, m_MinWidth=250, m_MinHeight=24, m_PreferredWidth=-1, m_PreferredHeight=-1, m_FlexibleWidth=1, m_FlexibleHeight=-1, m_LayoutPriority=1]
                    Children:
                        GameObject[BG, 0 child(ren), Layer 5, Active=True]
                            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                            Component[ImageToggleState, TargetImage=BG (KImage), ActiveSprite=web_title (UnityEngine.Sprite), InactiveSprite=web_title (UnityEngine.Sprite), DisabledSprite=null, DisabledActiveSprite=null, useSprites=False, ActiveColour=RGBA(0.286, 0.286, 0.286, 1.000), InactiveColour=RGBA(0.286, 0.286, 0.286, 1.000), DisabledColour=RGBA(0.286, 0.286, 0.286, 1.000), DisabledActiveColour=RGBA(0.000, 0.000, 0.000, 0.000), HoverColour=RGBA(0.000, 0.000, 0.000, 0.000), DisabledHoverColor=RGBA(0.500, 0.500, 0.500, 1.000), colorStyleSetting=colorstyle_darkBG (ColorStyleSetting), isActive=False, currentState=Inactive, useStartingState=False, startingState=Inactive]
                            Component[KImage, Color=RGBA(0.286, 0.286, 0.286, 1.000), Sprite=web_title (UnityEngine.Sprite), defaultState=Active, colorSelector=Inactive, colorStyleSetting=null, clearMaskOnDisable=True]
                        GameObject[Label, 0 child(ren), Layer 5, Active=True]
                            Translation=(3238.87, -475.48, 0.00) [(5.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(-10.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(10.00,0.00) OffsetMax=(0.00,0.00)]
                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                            Component[LocText, Text=_Select Facade:, Color=RGBA(1.000, 1.000, 1.000, 1.000), Font=Economica-Bold-OTF SDF (TMPro.TMP_FontAsset), key=STRINGS.UI.FACADE_SELECTION_PANEL.HEADER, textStyleSetting=style_titleTextSmall (TextStyleSetting), allowOverride=False, staticLayout=False, textLinkHandler=null, originalString=, allowLinksInternal=False]
                            Component[SetTextStyleSetting, text=null, sdfText=null, style=style_titleTextSmall (TextStyleSetting), currentStyle=null]
                GameObject[Container, 1 child(ren), Layer 5, Active=True]
                    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                    Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=web_box (UnityEngine.Sprite), m_Sprite=web_box (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                    Component[UnityEngine.UI.VerticalLayoutGroup, Child Align=UpperLeft, Control W=True, Control H=True]
                    Children:
                        GameObject[Content, 6 child(ren), Layer 5, Active=True]
                            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                            Rect[Size=(250.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.00,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(250.00,0.00)]
                            Component[UnityEngine.UI.GridLayoutGroup, m_StartCorner=UpperLeft, m_StartAxis=Horizontal, m_CellSize=(78.00, 78.00), m_Spacing=(4.00, 4.00), m_Constraint=FixedColumnCount, m_ConstraintCount=3]
                            Component[UnityEngine.UI.ContentSizeFitter, m_HorizontalFit=PreferredSize, m_VerticalFit=PreferredSize, m_Rect=null, m_Tracker=UnityEngine.DrivenRectTransformTracker]
                            Children:
                                GameObject[FacadeTogglePrefab, 4 child(ren), Layer 5, Active=False]
                                    Translation=(3302.05, -546.96, 0.00) [(43.00, -43.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-82.00) OffsetMax=(82.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[MultiToggle, states=[ToggleState,ToggleState], play_sound_on_click=True, play_sound_on_release=False, toggle_image=BGImage (UnityEngine.UI.Image), state=0, onClick=null, stateDirty=True, onDoubleClick=null, onEnter=null, onExit=null, onHold=null, onStopHold=null, allowRightClick=True, clickHeldDown=False, totalHeldTime=0, heldTimeThreshold=0.4, pointerOver=False]
                                    Component[HierarchyReferences, references=[ElementReference,ElementReference,ElementReference]]
                                    Component[ToolTip, UseFixedStringKey=False, FixedStringKey=, multiStringToolTips=[], styleSettings=[], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=False]
                                            Translation=(3302.05, -546.96, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=web_button (UnityEngine.Sprite), m_Sprite=web_button (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=False]
                                            Translation=(3302.05, -546.96, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=codexIconBuildings (UnityEngine.Sprite), m_Sprite=codexIconBuildings (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                        GameObject[Mannequin, 0 child(ren), Layer 5, Active=False]
                                            Translation=(3302.05, -546.96, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(78.00,78.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UIMannequin, animController=null, spawn=null, shouldShowOutfitWithDefaultItems=True, personalityToUseForDefaultClothing=None]
                                        GameObject[DlcBanner, 0 child(ren), Layer 5, Active=False]
                                            Translation=(3290.41, -482.13, 0.00) [(-7.00, 39.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(-1.00, 1.00, 1.00)
                                            Rect[Size=(64.00,64.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,1.00) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(0.00,-64.00) OffsetMax=(64.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=dlc_banner (UnityEngine.Sprite), m_Sprite=dlc_banner (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[GetMoreToggle, 2 child(ren), Layer 5, Active=False]
                                    Translation=(3277.11, -523.69, 0.00) [(28.00, -29.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(48.00,50.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,1.00) AnchorMax=(0.00,1.00) OffsetMin=(4.00,-54.00) OffsetMax=(52.00,-4.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Component[ToolTip, UseFixedStringKey=True, FixedStringKey=STRINGS.UI.FACADE_SELECTION_PANEL.STORE_BUTTON_TOOLTIP, multiStringToolTips=[], styleSettings=[], worldSpace=False, forceRefresh=False, refreshWhileHovering=False, <isHovering>k__BackingField=False, lastUpdateTime=0, toolTipPosition=Custom, tooltipPivot=(1.00, 0.00), tooltipPositionOffset=(-8.00, 0.00), parentPositionAnchor=(0.00, 0.00), overrideParentObject=BodySkin (UnityEngine.RectTransform), SizingSetting=DynamicWidthNoWrap, WrapWidth=256, _OnToolTip=null, OnComplexToolTip=null]
                                    Component[KButton, soundPlayer=ButtonSoundPlayer, bgImage=BGImage (KImage), fgImage=null, additionalKImages=[], onClick=null, onDoubleClick=null, onBtnClick=null, onPointerEnter=null, onPointerExit=null, onPointerDown=null, onPointerUp=null, interactable=True, mouseOver=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=False]
                                            Translation=(3277.11, -523.69, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(48.00,50.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[KImage, Color=RGBA(0.529, 0.272, 0.401, 1.000), Sprite=web_button (UnityEngine.Sprite), defaultState=Inactive, colorSelector=Inactive, colorStyleSetting=colorstyle_button (ColorStyleSetting), clearMaskOnDisable=True]
                                        GameObject[FGImage, 0 child(ren), Layer 5, Active=False]
                                            Translation=(3277.11, -523.69, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(44.00,46.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(2.00,2.00) OffsetMax=(-2.00,-2.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(1.000, 1.000, 1.000, 1.000), Sprite=icon_display_screen_blueprint (UnityEngine.Sprite), m_Sprite=icon_display_screen_blueprint (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Simple, m_PreserveAspect=True, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[DummyPlaceholder, 1 child(ren), Layer 5, Active=True]
                                    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.341, 0.341, 0.341, 0.322), Sprite=web_border (UnityEngine.Sprite), m_Sprite=web_border (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[DummyPlaceholder (1), 1 child(ren), Layer 5, Active=True]
                                    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.341, 0.341, 0.341, 0.322), Sprite=web_border (UnityEngine.Sprite), m_Sprite=web_border (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[DummyPlaceholder (2), 1 child(ren), Layer 5, Active=True]
                                    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.341, 0.341, 0.341, 0.322), Sprite=web_border (UnityEngine.Sprite), m_Sprite=web_border (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
                                GameObject[DummyPlaceholder (3), 1 child(ren), Layer 5, Active=True]
                                    Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                    Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(0.00,0.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                    Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                    Children:
                                        GameObject[BGImage, 0 child(ren), Layer 5, Active=True]
                                            Translation=(3230.56, -475.48, 0.00) [(0.00, 0.00, 0.00)] Rotation=(0.00000, 0.00000, 0.00000, 1.00000) [(0.00000, 0.00000, 0.00000, 1.00000)] Scale=(1.00, 1.00, 1.00)
                                            Rect[Size=(0.00,0.00) Min=(0.00,0.00) Preferred=(0.00,0.00) Flexible=(0.00,0.00) Pivot=(0.50,0.50) AnchorMin=(0.00,0.00) AnchorMax=(1.00,1.00) OffsetMin=(0.00,0.00) OffsetMax=(0.00,0.00)]
                                            Component[UnityEngine.CanvasRenderer, <isMask>k__BackingField=False]
                                            Component[UnityEngine.UI.Image, Color=RGBA(0.341, 0.341, 0.341, 0.322), Sprite=web_border (UnityEngine.Sprite), m_Sprite=web_border (UnityEngine.Sprite), m_OverrideSprite=null, m_Type=Sliced, m_PreserveAspect=False, m_FillCenter=True, m_FillMethod=Radial360, m_FillAmount=1, m_FillClockwise=True, m_FillOrigin=0, m_AlphaHitTestMinimumThreshold=0, m_Tracked=False, m_UseSpriteMesh=False, m_PixelsPerUnitMultiplier=1, m_CachedReferencePixelsPerUnit=100]
```
