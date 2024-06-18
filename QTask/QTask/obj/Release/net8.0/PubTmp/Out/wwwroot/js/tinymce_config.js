



function LoadTinyMCE() {

    tinymce.init({
        selector: '.div-editor',
        inline: true,
        convert_urls: true,
        menubar: "table view insert edit tools",

        plugins: [
            "advlist autolink lists link image charmap preview hr anchor pagebreak",
            "searchreplace wordcount visualblocks visualchars code fullscreen",
            "insertdatetime media nonbreaking save table contextmenu directionality",
            "template paste textcolor moxiemanager"
        ],
        toolbar1: "template undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link insertfile",
        toolbar2: "preview media | forecolor backcolor emoticons",
        
        forced_root_block: "",
        force_p_newlines: false,
        force_br_newlines: false,
        remove_trailing_brs: false,
        cleanup_on_startup: false,
        trim_span_elements: false,
        verify_html: false,
        cleanup: false,
        relative_urls: false,
        remove_script_host: true,
        
        //document_base_url: 'https://www.quantumamc.com/',
//        moxiemanager_image_settings:{
//            /* Scope to different folder, show thumbnails of selected extensions */
//            moxiemanager_title : 'Images',
//            moxiemanager_extensions : 'jpg,png,gif',
//            moxiemanager_rootpath: 'https://www.quantumamc.com/',
//    moxiemanager_view : 'thumbs'
//}

    });

    
   
}