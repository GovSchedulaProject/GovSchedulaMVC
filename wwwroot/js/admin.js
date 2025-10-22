function openModal(modalId) {
    document.getElementById(modalId).style.display = 'block';
}

function closeModal(modalId) {
    document.getElementById(modalId).style.display = 'none';
}

// Close modal if user clicks outside of it
window.onclick = function(event) {
    if (event.target.classList.contains('admin-modal')) {
        event.target.style.display = 'none';
    }
}