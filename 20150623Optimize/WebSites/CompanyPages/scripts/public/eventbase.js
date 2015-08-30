var EventBase = function() {
	this.addListener = function ( type, listener ) {
		getListener( this, type, true ).push( listener );
	}	

	this.removeListener = function ( type, listener ) {
		var listeners = getListener( this, type );
		listeners && utils.removeItem( listeners, listener );
	}	
	
	this.fireEvent = function ( type ) {
		var listeners = getListener( this, type ),
			r, t, k;
		if ( listeners ) {
			k = listeners.length;
			while ( k -- ) {
				t = listeners[k].apply( this, arguments );
				if ( t !== undefined ) {
					r = t;
				}
			}    
		}
		if ( t = this['on' + type.toLowerCase()] ) {
			r = t.apply( this, arguments );
		}
		return r;
	}		
}
function getListener( obj, type, force ) {
	var allListeners;
	type = type.toLowerCase();
	return ( ( allListeners = ( obj.__allListeners || force && ( obj.__allListeners = {} ) ) )
		&& ( allListeners[type] || force && ( allListeners[type] = [] ) ) );
}


var EventUtil = {

    addHandler: function(element, type, handler){
        if (element.addEventListener){
            element.addEventListener(type, handler, false);
        } else if (element.attachEvent){
            element.attachEvent("on" + type, handler);
        } else {
            element["on" + type] = handler;
        }
    },
    
    getButton: function(event){
        if (document.implementation.hasFeature("MouseEvents", "2.0")){
            return event.button;
        } else {
            switch(event.button){
                case 0:
                case 1:
                case 3:
                case 5:
                case 7:
                    return 0;
                case 2:
                case 6:
                    return 2;
                case 4: return 1;
            }
        }
    },
    
    getCharCode: function(event){
        if (typeof event.charCode == "number"){
            return event.charCode;
        } else {
            return event.keyCode;
        }
    },
    
    getClipboardText: function(event){
        var clipboardData =  (event.clipboardData || window.clipboardData);
        return clipboardData.getData("text");
    },
    
    getEvent: function(event){
        return event ? event : window.event;
    },
    
    getRelatedTarget: function(event){
        if (event.relatedTarget){
            return event.relatedTarget;
        } else if (event.toElement){
            return event.toElement;
        } else if (event.fromElement){
            return event.fromElement;
        } else {
            return null;
        }
    
    },
    
    getTarget: function(event){
        if (event.target){
            return event.target;
        } else {
            return event.srcElement;
        }
    },
    
    getWheelDelta: function(event){
        if (event.wheelDelta){
            return (client.opera && client.opera < 9.5 ? -event.wheelDelta : event.wheelDelta);
        } else {
            return -event.detail * 40;
        }
    },
    
    preventDefault: function(event){
        if (event.preventDefault){
            event.preventDefault();
        } else {
            event.returnValue = false;
        }
    },

    removeHandler: function(element, type, handler){
        if (element.removeEventListener){
            element.removeEventListener(type, handler, false);
        } else if (element.detachEvent){
            element.detachEvent("on" + type, handler);
        } else {
            element["on" + type] = null;
        }
    },
    
    setClipboardText: function(event, value){
        if (event.clipboardData){
            event.clipboardData.setData("text/plain", value);
        } else if (window.clipboardData){
            window.clipboardData.setData("text", value);
        }
    },
    
    stopPropagation: function(event){
        if (event.stopPropagation){
            event.stopPropagation();
        } else {
            event.cancelBubble = true;
        }
    }

};
