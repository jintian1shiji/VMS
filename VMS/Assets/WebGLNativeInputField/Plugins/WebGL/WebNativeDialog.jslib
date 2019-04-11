var WebNativeDialog = {
  SetupOverlayDialogHtml:function(defaultValue,x,y,w,h,s){
    defaultValue = Pointer_stringify(defaultValue);
	
    if( !document.getElementById("nativeInputDialogInput" ) ){
      // setup css
      var style = document.createElement( 'style' );
      style.setAttribute('id' , 'inputDialogTextSelect');
      style.appendChild( document.createTextNode( 'nativeInputDialogInput::-moz-selection { background-color:#00ffff;}' ) );
      style.appendChild( document.createTextNode( 'nativeInputDialogInput::selection { background-color:#00ffff;}' ) );
      document.head.appendChild( style );
    }
    if( !document.getElementById("nativeInputDialog" ) ){
      // setup html
      var html = '<div id="nativeInputDialog" '+
	  'style="background:#0000003F;opacity:1;width:100%;height:100%;position:fixed;top:0%;z-index:2147483647;">' + 
               '    <input id="nativeInputDialogInput" type="text" onsubmit="" style="position:absolute;background:#FFFFFF7F;">' + 
               '    <div style="margin-top:10px">' + 
               '      <input id="nativeInputDialogCheck" type="checkBox" style="display:none;">' + 
               '    </div>' + 
               '</div>';
      var element = document.createElement('div');
      element.innerHTML = html;
      // write to html
      document.getElementById("gameContainer").appendChild(element);
		//document.body.appendChild(element);
      // set Event
      var okFunction = 
        'document.getElementById("nativeInputDialog" ).style.display = "none";' + 
        'document.getElementById("nativeInputDialogCheck").checked = false;' +
        'document.getElementById("#canvas").style.display="";'+
		'event.stopPropagation();';
      var cancelFunction = 
        'document.getElementById("nativeInputDialog" ).style.display = "none";'+ 
        'document.getElementById("nativeInputDialogCheck").checked = true;'+
        'document.getElementById("#canvas").style.display="";';
	  var inputClickFunc = 'event.stopPropagation();';
      var inputField = document.getElementById("nativeInputDialogInput");
      inputField.setAttribute( "onkeyup" , 'if(event.keyCode==13) {'+ okFunction +'} else if(event.keyCode==27){'+cancelFunction+'}'  );
      inputField.setAttribute( "onclick" , inputClickFunc );
      var okBtn = document.getElementById("nativeInputDialog");
      okBtn.setAttribute( "onclick" , okFunction );
    }
    var dinput = document.getElementById("nativeInputDialogInput");
	dinput.value= defaultValue;
	dinput.style.left = x+'px';
	dinput.style.top = y+'px';
	dinput.style.width = w+'px';
	dinput.style.height = h+'px';
	dinput.style.fontSize = s+'px';
	dinput.focus();
	//dinput.select();

    document.getElementById("nativeInputDialog" ).style.display = "";
  },
  HideUnityScreenIfHtmlOverlayCant:function(){
    if( navigator.userAgent.indexOf("Chrome") < 0 ){
     // document.getElementById("#canvas").style.display="none";
    }
  },
  IsRunningOnEdgeBrowser:function(){
    if( navigator.userAgent.indexOf("Edge") < 0 ){
      return false;
    }
    return true;
  },
  IsOverlayDialogHtmlActive:function(){
    var nativeDialog = document.getElementById("nativeInputDialog" );
    if( !nativeDialog ){
      return false;
    }
    return ( nativeDialog.style.display != 'none' );
  },
  IsOverlayDialogHtmlCanceled:function(){
    var check = document.getElementById("nativeInputDialogCheck");
    if( !check ){ return false; }
    return check.checked;
  },
  GetOverlayHtmlInputFieldValue:function(){
    var inputField = document.getElementById("nativeInputDialogInput");
    var result = "";
    if( inputField && inputField.value ){
      result = inputField.value;
    }
    var bufferSize = lengthBytesUTF8(result) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(result, buffer, bufferSize);
    return buffer;
  }

};
mergeInto( LibraryManager.library , WebNativeDialog );

