const uploadFile = document.getElementById("proposal-file");
const submitProposal = document.getElementById("submit-proposal");
const removeProposal = document.getElementById("remove-proposal");
const uploadReport = document.getElementById("upload-report");
const submitReport = document.getElementById("submit-report");
const removeReport = document.getElementById("remove-report");
const proposalForm = document.getElementById("proposal-form")

uploadFile.addEventListener('change', ()=>{
    if(uploadFile.value){
        removeProposal.removeAttribute("hidden");
    }

    submitProposal.removeAttribute("disabled");

})

proposalForm.addEventListener('submit', ()=>{
    uploadReport.removeAttribute("disabled");
})

removeProposal.addEventListener('click', ()=>{
    uploadFile.value = '';
});

