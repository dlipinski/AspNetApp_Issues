// Write your JavaScript code.


function appendSolution(id) {
    const display = document.getElementById(id).style.display;
    if(display === 'none'){ 
        document.getElementById(id).style.display='block';
    } else {
        document.getElementById(id).style.display='none';
    }
}


function id_text_confirm(id, text){

    if (confirm(text)) {
        document.getElementById(id).submit();
    } else {
        
    }

}