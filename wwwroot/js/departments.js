document.addEventListener('DOMContentLoaded', function () {
    const modalBackdrop = document.getElementById('serviceModalBackdrop');
    const modal = document.getElementById('serviceModal');
    const modalDeptName = document.getElementById('modalDeptName');
    const modalServiceList = document.getElementById('modalServiceList');
    const modalCloseButton = document.getElementById('modalCloseButton');
    const bookButtons = document.querySelectorAll('.bookButton');
    const departmentsDataContainer = document.getElementById('departmentsData');

    // Function to show the modal
    function showModal(deptId, deptName) {
        // Clear previous services
        modalServiceList.innerHTML = '';

        // Set department name
        modalDeptName.textContent = deptName;

        // Find the hidden service data for this department
        const deptDataElement = departmentsDataContainer.querySelector(`#dept-${deptId}`);
        if (deptDataElement) {
            const serviceLinks = deptDataElement.querySelectorAll('a');
            serviceLinks.forEach(link => {
                const serviceName = link.getAttribute('data-name');
                const serviceUrl = link.getAttribute('href');

                // Create new link element for the modal list
                const newLink = document.createElement('a');
                newLink.href = serviceUrl;
                newLink.textContent = serviceName;
                newLink.className = 'serviceLink'; // Use the class from CSS
                modalServiceList.appendChild(newLink);
            });
        }

        modalBackdrop.style.display = 'flex'; // Show backdrop and modal
    }

    // Function to hide the modal
    function hideModal() {
        modalBackdrop.style.display = 'none';
    }

    // Add event listeners to all "Book Appointment" buttons
    bookButtons.forEach(button => {
        button.addEventListener('click', function () {
            const deptId = this.getAttribute('data-dept-id');
            const deptName = this.getAttribute('data-dept-name');
            showModal(deptId, deptName);
        });
    });

    // Add event listeners to close the modal
    modalCloseButton.addEventListener('click', hideModal);
    modalBackdrop.addEventListener('click', hideModal);

    // Prevent clicks inside the modal from closing it
    modal.addEventListener('click', function (event) {
        event.stopPropagation();
    });

});