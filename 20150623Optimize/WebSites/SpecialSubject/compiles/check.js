var check = new Class(function(querys, getforms){
	var forms = getforms();
	if ( forms.password === 'viewalloc' ){
		Session('maker') = true;
		Response.Redirect(iPress.setURL('maker', 'web'));
	}else{
		return {};
	}
});


module.exports = check;