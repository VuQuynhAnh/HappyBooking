window.startVideoCall = async function (groupId, dotNetHelper) {
    try {
        let localStream = await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
        document.getElementById("localVideo").srcObject = localStream;

        let peerConnection = new RTCPeerConnection();

        // Thêm các track từ localStream vào peerConnection
        localStream.getTracks().forEach(track => peerConnection.addTrack(track, localStream));

        // Lắng nghe các sự kiện ICE Candidate
        peerConnection.onicecandidate = event => {
            if (event.candidate) {
                dotNetHelper.invokeMethodAsync('SendIceCandidate', groupId, JSON.stringify(event.candidate));
            }
        };

        // Lắng nghe stream remote
        peerConnection.ontrack = event => {
            const remoteVideo = document.getElementById("remoteVideo");
            remoteVideo.srcObject = event.streams[0];
        };

        // Tạo offer và gửi cho server
        const offer = await peerConnection.createOffer();
        await peerConnection.setLocalDescription(new RTCSessionDescription(offer));
        dotNetHelper.invokeMethodAsync('SendOffer', groupId, JSON.stringify(offer));

        // Nhận answer từ server
        dotNetHelper.on('ReceiveAnswer', async (answer) => {
            await peerConnection.setRemoteDescription(new RTCSessionDescription(JSON.parse(answer)));
        });

        // Nhận ICE candidate từ server
        dotNetHelper.on('ReceiveIceCandidate', async (candidate) => {
            await peerConnection.addIceCandidate(new RTCIceCandidate(JSON.parse(candidate)));
        });

    } catch (error) {
        console.error("Error accessing media devices.", error);
    }
};
