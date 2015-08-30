function uploadStart(file) {
    try {
	if (VA.Util.disManageType === 'mutil') {
	    var upload = document.getElementById("upload");
	    upload.style.position = "absolute";
	    upload.style.left = "-10000px";
	    document.getElementById("mutilEdit").style.display = "block";
	}
	$(".pre-upload-loading").css("display", "block");
    } catch (ex) {
	this.debug(ex);
    }
}

function fileQueueError(file, errorCode, message) {
    try {
	var imageName = "error.gif";
	var errorName = "";
	if (errorCode === SWFUpload.errorCode_QUEUE_LIMIT_EXCEEDED) {
	    errorName = "对不起你一次性上传了 10 张以上图片，数量太多了；";
	}

	// if (errorName !== "") {
	//    alert(errorName);
	//   return;
	// }

	switch (errorCode) {
	    case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
		imageName = "zerobyte.gif";
		break;
	    case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
		// imageName = "toobig.gif";
		var imgCapacity = VA.renderPage.get('dataConfig').dishImage.space.slice(0,-2);
	        var imgCapacityStr = Math.round(imgCapacity/1024*100)/100;
		alert("请上传小于 "+imgCapacityStr+"M 的图片.");
		break;
	    case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
	    case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
	    default:
		alert(message);
		break;
	}
	// addImage("images/" + imageName); // 失败时不再加载新图片从显示； 
    } catch (ex) {
	this.debug(ex);
    }

}

function fileDialogComplete(numFilesSelected, numFilesQueued) {
	try {
		if (numFilesQueued > 0) {
			this.startUpload();
		}
	} catch (ex) {
		this.debug(ex);
	}
}

function uploadProgress(file, bytesLoaded) {

	try {
		var percent = Math.ceil((bytesLoaded / file.size) * 100);

		var progress = new FileProgress(file,  this.customSettings.upload_target);
		progress.setProgress(percent);
		if (percent === 100) {
			progress.setStatus("Creating thumbnail...");
			progress.toggleCancel(false, this);
		} else {
			progress.setStatus("Uploading...");
			progress.toggleCancel(true, this);
		}
	} catch (ex) {
		this.debug(ex);
	}
}

function uploadSuccess(file, serverData) {
    //try {
    var uploadMsg = serverData;
    if (uploadMsg.indexOf("-")>-1) {
	if (VA.Util.disManageType == 'mutil') {//url
	    var imgData = eval("(" + serverData + ")");
            var upload = document.getElementById("upload");
	    upload.style.position = "absolute";
	    upload.style.left = "-10000px";
	    document.getElementById("mutilEdit").style.display = "block";
	    
	    VA.Util.picStatus = '2';
	    if (!imgData.length) {
		addImage(imgData);
	    } else {
		for (var i = 0; i < imgData.length; i++) {
		    addImage(imgData[i]);
		}
	    }
	    ;
	} else {//sessionID
	    VA.Util.sessionID = serverData;
	    VA.Util.picStatus = '2';
	    var imgData = new Object();
	    imgData.Url = "thumbnail.aspx?id=" + serverData;
	    addImage(imgData);
	}
    }else{
	var msg = uploadMsg.split(",");
	//if( msg[0]==="imageRatioError" ){
	    alert(msg[1]);
	//}
    }

    //} catch (ex) {
    //	this.debug(ex);
    //}
}

function uploadComplete(file) {
    try {
	$(".pre-upload-loading").css("display","none");
	/*  I want the next upload to continue automatically so I'll call startUpload here */
	if (this.getStats().files_queued > 0) {
	    this.startUpload();// 启动下一个文件的自动上传；
	} else {
	    var progress = new FileProgress(file, this.customSettings.upload_target);
	    progress.setComplete();
	    progress.setStatus("All images received.");
	    progress.toggleCancel(false);
	    //
	}
    } catch (ex) {
	this.debug(ex);
    }
}

function uploadError(file, errorCode, message) {
    var imageName = "error.gif";
    var progress;
    try {
	switch (errorCode) {
	    case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
		try {
		    progress = new FileProgress(file, this.customSettings.upload_target);
		    progress.setCancelled();
		    progress.setStatus("Cancelled");
		    progress.toggleCancel(false);
		}
		catch (ex1) {
		    this.debug(ex1);
		}
		break;
	    case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
		try {
		    progress = new FileProgress(file, this.customSettings.upload_target);
		    progress.setCancelled();
		    progress.setStatus("Stopped");
		    progress.toggleCancel(true);
		}
		catch (ex2) {
		    this.debug(ex2);
		}
	    case SWFUpload.UPLOAD_ERROR.UPLOAD_LIMIT_EXCEEDED:
		imageName = "uploadlimit.gif";
		break;
	    default:
		alert(message);
		break;
	}
	addImage("images/" + imageName);

    } catch (ex3) {
	this.debug(ex3);
    }
}

function addImage(imgData) {//裁剪弹窗 添加图片
	var src = imgData.Url;
	if(VA.Util.disManageType=='mutil'){//批量上传
	    var container = document.getElementById("uploadList"),
		    imgSprite = document.createElement("li"),
		    newImg = document.createElement("img"),
		    arrow = document.createElement("i");
	    newImg.style.width = "191px";
	    newImg.style.height = "143px";
	    newImg.setAttribute('id',imgData.Id);
	    newImg.setAttribute('rel',imgData.Name);
	    newImg.src = "images/nonImg.png";
	    arrow.className = "icon";
	    if(!imgData.EqualProportion){
		imgSprite.appendChild(arrow);
	    }
	    imgSprite.appendChild(newImg);
	    container.appendChild(imgSprite);
	    if(container.childNodes.length<=1){//一张
		dishManageMutilMethod.deletePic();//初次侦听裁图弹层 uploadHandler();
		dishManageMutilMethod.getPicName();
	    };
	}else{
		var sprite = document.getElementById("uploadContent");
		var newImg = sprite.getElementsByTagName('img')[0];
		var preview = sprite.getElementsByTagName('img')[1];//,
		// previewSec = sprite.getElementsByTagName('img')[2];
		preview.src = src;
		// previewSec.src = src;
	}
	newImg.style.margin = "0px";

	if (newImg.filters) {
		try {
			newImg.filters.item("DXImageTransform.Microsoft.Alpha").opacity = 0;
		} catch (e) {
			newImg.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=' + 0 + ')';
		}
	} else {
		newImg.style.opacity =0;
	}
	var tempImg = new Image();
	tempImg.onload = function(){
	    fadeIn(newImg, 0);
	    YUI().use('login', 'loginModule', 'dataTablePack', 'datatype', 'event-move', function(Y) {
		if(VA.Util.disManageType=='mutil'){
		    // dishManageMutilMethod.
		}else{
		    VA.renderPage.DisUploadItem.setImgAreaObjOptions( tempImg.width );
		}
	    });
	};
	tempImg.src= src;
	newImg.src = src;
}

function fadeIn(element, opacity) {
	var reduceOpacityBy = 5;
	var rate = 30;	// 15 fps
	if (opacity < 100) {
		opacity += reduceOpacityBy;
		if (opacity > 100) {
			opacity = 100;
		}

		if (element.filters) {
			try {
				element.filters.item("DXImageTransform.Microsoft.Alpha").opacity = opacity;
			} catch (e) {
				// If it is not set initially, the browser will throw an error.  This will set it if it is not set yet.
				element.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=' + opacity + ')';
			}
		} else {
			element.style.opacity = opacity / 100;
		}
	}

	if (opacity < 100) {
		setTimeout(function () {
			fadeIn(element, opacity);
		}, rate);
	}
}



/* ******************************************
 *	FileProgress Object
 *	Control object for displaying file info
 * ****************************************** */

function FileProgress(file, targetID) {
	this.fileProgressID = "divFileProgress";

	this.fileProgressWrapper = document.getElementById(this.fileProgressID);
	if (!this.fileProgressWrapper) {
		this.fileProgressWrapper = document.createElement("div");
		this.fileProgressWrapper.className = "progressWrapper";
		this.fileProgressWrapper.id = this.fileProgressID;

		this.fileProgressElement = document.createElement("div");
		this.fileProgressElement.className = "progressContainer";

		var progressCancel = document.createElement("a");
		progressCancel.className = "progressCancel";
		progressCancel.href = "#";
		progressCancel.style.visibility = "hidden";
		progressCancel.appendChild(document.createTextNode(" "));

		var progressText = document.createElement("div");
		progressText.className = "progressName";
		progressText.appendChild(document.createTextNode(file.name));

		var progressBar = document.createElement("div");
		progressBar.className = "progressBarInProgress";

		var progressStatus = document.createElement("div");
		progressStatus.className = "progressBarStatus";
		progressStatus.innerHTML = "&nbsp;";

		this.fileProgressElement.appendChild(progressCancel);
		this.fileProgressElement.appendChild(progressText);
		this.fileProgressElement.appendChild(progressStatus);
		this.fileProgressElement.appendChild(progressBar);

		this.fileProgressWrapper.appendChild(this.fileProgressElement);

		document.getElementById(targetID).appendChild(this.fileProgressWrapper);
		fadeIn(this.fileProgressWrapper, 0);

	} else {
		this.fileProgressElement = this.fileProgressWrapper.firstChild;
		this.fileProgressElement.childNodes[1].firstChild.nodeValue = file.name;
	}

	this.height = this.fileProgressWrapper.offsetHeight;

}
FileProgress.prototype.setProgress = function (percentage) {
	this.fileProgressElement.className = "progressContainer green";
	this.fileProgressElement.childNodes[3].className = "progressBarInProgress";
	this.fileProgressElement.childNodes[3].style.width = percentage + "%";
};
FileProgress.prototype.setComplete = function () {
	this.fileProgressElement.className = "progressContainer blue";
	this.fileProgressElement.childNodes[3].className = "progressBarComplete";
	this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setError = function () {
	this.fileProgressElement.className = "progressContainer red";
	this.fileProgressElement.childNodes[3].className = "progressBarError";
	this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setCancelled = function () {
	this.fileProgressElement.className = "progressContainer";
	this.fileProgressElement.childNodes[3].className = "progressBarError";
	this.fileProgressElement.childNodes[3].style.width = "";

};
FileProgress.prototype.setStatus = function (status) {
	this.fileProgressElement.childNodes[2].innerHTML = status;
};

FileProgress.prototype.toggleCancel = function (show, swfuploadInstance) {
	this.fileProgressElement.childNodes[0].style.visibility = show ? "visible" : "hidden";
	if (swfuploadInstance) {
		var fileID = this.fileProgressID;
		this.fileProgressElement.childNodes[0].onclick = function () {
			swfuploadInstance.cancelUpload(fileID);
			return false;
		};
	}
};
