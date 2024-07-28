window.createObjectURLFromArray = function (fileBytes) {
    var blob = new Blob([fileBytes]);
    var url = URL.createObjectURL(blob);
    return url;
};

// wwwroot/js/pdfHelper.js
window.jsPDFHelper = {
    previewPDF: function (data, filename) {
        const { jsPDF } = window.jspdf;
        const doc = new jsPDF({
            unit: 'pt',
            format: 'a4'
        });

        // Template HTML với CSS nhúng và font chữ hỗ trợ tiếng Việt
        const template = `
            <div style="font-family: 'Arial', sans-serif; color: #333; padding: 20px; font-size: 12pt;">
                <div style="display: flex; align-items: center; margin-bottom: 20px;">
                    <img src="{{avatarUrl}}" style="border-radius: 50%; width: 100px; height: 100px; margin-left: 20px;" />
                    <div style="flex: 1;">
                        <h1 style="font-size: 16pt; margin: 0;">{{userName}}</h1>
                        <p style="font-size: 12pt; margin: 2px 0;">Email: {{userEmail}}</p>
                        <p style="font-size: 12pt; margin: 2px 0;">Phone: {{userPhone}}</p>
                    </div>
                </div>
                <div style="margin: 20px 0;">
                    <div style="margin-bottom: 20px;">
                        <h2 style="font-size: 14pt; border-bottom: 1px solid #333; padding-bottom: 5px; margin-bottom: 10px;">Citizen ID</h2>
                        <p style="font-size: 12pt; margin: 5px 0;">{{citizenId}}</p>
                    </div>
                    <div style="margin-bottom: 20px;">
                        <h2 style="font-size: 14pt; border-bottom: 1px solid #333; padding-bottom: 5px; margin-bottom: 10px;">Address</h2>
                        <p style="font-size: 12pt; margin: 5px 0;">{{address}}</p>
                    </div>
                </div>
            </div>
        `;

        // Replace placeholders with actual data
        const content = template
            .replace('{{userName}}', data.userName)
            .replace('{{userEmail}}', data.userEmail)
            .replace('{{userPhone}}', data.userPhone)
            .replace('{{citizenId}}', data.citizenId)
            .replace('{{address}}', data.address)
            .replace('{{avatarUrl}}', data.avatarUrl);

        // Sanitize HTML content
        const cleanContent = DOMPurify.sanitize(content);

        // Render HTML content in PDF
        const pdfContainer = document.createElement('div');
        pdfContainer.innerHTML = cleanContent;
        document.body.appendChild(pdfContainer);

        doc.html(pdfContainer, {
            callback: function (doc) {
                const blob = doc.output('blob');
                const url = URL.createObjectURL(blob);
                window.open(url);
                document.body.removeChild(pdfContainer);
            },
            x: 20,
            y: 20,
            width: 555, // Adjust width to fit A4 size
            windowWidth: 800,
            html2canvas: {
                scale: 2 // Increase scale to ensure image quality
            }
        });
    }
};
