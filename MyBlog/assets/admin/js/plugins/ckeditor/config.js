/**
 * @license Copyright (c) 2003-2021, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	
	// %REMOVE_START%
	// The configuration options below are needed when running CKEditor from source files.
	config.plugins = 'dialogui,dialog,about,a11yhelp,dialogadvtab,basicstyles,bidi,blockquote,notification,button,toolbar,clipboard,panelbutton,panel,floatpanel,colorbutton,colordialog,templates,menu,contextmenu,copyformatting,div,editorplaceholder,resize,elementspath,enterkey,entities,exportpdf,popup,filetools,filebrowser,find,fakeobjects,flash,floatingspace,listblock,richcombo,font,forms,format,horizontalrule,htmlwriter,iframe,wysiwygarea,image,indent,indentblock,indentlist,smiley,justify,menubutton,language,link,list,liststyle,magicline,maximize,newpage,pagebreak,pastetext,xml,ajax,pastetools,pastefromgdocs,pastefromlibreoffice,pastefromword,preview,print,removeformat,save,selectall,showblocks,showborders,sourcearea,specialchar,scayt,stylescombo,tab,table,tabletools,tableselection,undo,lineutils,widgetselection,widget,notificationaggregator,uploadwidget,uploadimage,textwatcher,autocomplete,textmatch,emoji,image2,autolink,video,tableresize,youtube,uploadfile,codesnippet,html5video';
	config.skin = 'moonocolor';
	// %REMOVE_END%

	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
	config.language = 'vi';
	config.extraPlugins = 'youtube';
	config.syntaxhighlight_lang = 'csharp';
	config.syntaxhighlight_hideControls = true;
	config.filebrowserImageUploadUrl = '/Data';
	config.width = 800;
	config.height = 600;
	config.filebrowserBrowseUrl = '/assets/admin/js/plugins/ckfinder/ckfinder.html';
	config.filebrowserImageBrowseUrl = '/assets/admin/js/plugins/ckfinder.html?Type=Images';
	config.filebrowserFlashBrowseUrl = '/assets/admin/js/plugins/ckfinder.html?Type=Flash';
	config.filebrowserUploadUrl = '/assets/admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files&responseType=json';
	config.filebrowserImageUploadUrl = '/Data';
	config.filebrowserFlashUploadUrl = '/assets/admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

	CKFinder.setupCKEditor(null, '/assets/admin/js/plugins/ckfinder/');
};
