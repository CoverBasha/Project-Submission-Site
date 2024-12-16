const uploadFile = document.getElementById("proposal-file");
const submitProposal = document.getElementById("submit-proposal");
const removeProposal = document.getElementById("remove-proposal");



uploadFile.addEventListener('change', ()=>{
    if(uploadFile.value){
        removeProposal.removeAttribute("hidden");
    }

    submitProposal.removeAttribute("disabled");

})

removeProposal.addEventListener('click', ()=>{
    uploadFile.value = '';
});

