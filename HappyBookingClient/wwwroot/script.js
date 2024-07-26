window.createObjectURLFromArray = function (fileBytes) {
    var blob = new Blob([fileBytes]);
    var url = URL.createObjectURL(blob);
    return url;
};

window.jsPDFHelper = {
    generatePDF: function (data, filename) {
        const { jsPDF } = window.jspdf;
        const doc = new jsPDF();

        // Font Unicode để hỗ trợ tiếng Việt
        doc.addFont('https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.70/fonts/Roboto/Roboto-Regular.ttf', 'Roboto', 'normal');
        doc.setFont('Roboto');
        doc.setFontSize(12);

        // Template HTML với CSS
        const template = `
            <style>
                h1 {
                    color: #2c3e50;
                    text-align: center;
                }
                p {
                    font-size: 12px;
                    line-height: 1.5;
                    color: #34495e;
                }
                img {
                    display: block;
                    margin: 10px auto;
                    border-radius: 50%;
                }
            </style>
            <h1>User Information</h1>
            <p>Name: {{userName}}</p>
            <p>Email: {{userEmail}}</p>
            <p>Phone: {{userPhone}}</p>
            <p>Citizen ID: {{citizenId}}</p>
            <p>Address: {{address}}</p>
            <img src="{{avatarUrl}}" width="100" height="100" />
        `;

        // Replace placeholders with actual data
        const content = template
            .replace('{{userName}}', data.userName)
            .replace('{{userEmail}}', data.userEmail)
            .replace('{{userPhone}}', data.userPhone)
            .replace('{{citizenId}}', data.citizenId)
            .replace('{{address}}', data.address)
            .replace('{{avatarUrl}}', data.avatarUrl);

        // Render HTML content in PDF
        doc.html(content, {
            callback: function (doc) {
                doc.save(filename);
            },
            x: 10,
            y: 10
        });
    }
};


