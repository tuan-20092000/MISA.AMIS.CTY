<template>
    <div>
        <div v-if="loading" class="wrap-message-form">
            <img style="" src="../Resource/loading.svg" alt="">
        </div>
        <div v-if="warning" class="wrap-message-form">
            <div class="message-box">
                <div class="div-content">
                    <div class="icon-message">
                        <img src="../Resource/img/warning.svg" alt="">
                    </div>
                    <div class="message-content">
                        <div>Bạn có chắc muốn xóa nhân viên "{{employee.employeeName}}"
                            , mã nhân viên là "{{employee.employeeCode}}" không?</div>
                    </div>
                </div>
                <div class="space"></div>
                <div class="mess-footer">
                    <button v-on:click="cancelMessageBox()" class="btn-no">Hủy</button>
                    <button v-on:click="deleteEmployee()" class="btn-ok">Có</button>
                </div>
            </div>
        </div>
        <div v-if="error" class="wrap-message-form">
            <div class="message-box">
                <div class="div-content">
                    <div class="icon-message">
                        <img src="../Resource/img/warning.svg" alt="">
                    </div>
                    <div class="message-content">
                        <div> {{messageContent}}</div>
                    </div>
                    
                </div>
                <div class="space"></div>
                <div class="mess-footer">
                    <button v-on:click="cancelErrorBox()" class="btn-ok">Đồng ý</button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import EventBus from './../main.js';
export default {
    data() {
        return {
            warning: false,  // ẩn hiện form cảnh báo
            messageContent: null, // nội dung thông báo cho người dùng
            error: false, // ẩn hiện box báo lỗi
            field: null, // trường cần focus sau khi tắt message-box
            loading: true,
            // nhân viên hiển thị trên form warning
            employee : {
              employeeCode : null,
              employeeName : null,
              gender : 0,
              dateOfBirth : null,
              departmentName: null,
              identityDate : null,
              identityPlace : null,
              employeePosition : null,
              address : null,
              bankAccountNumber : null,
              bankName : null,
              bankBranchName : null,
              bankProvinceName : null,
              phoneNumber : null,
              telephoneNumber : null,
              email : null,
            },
        }
    },

    methods:{
        // hàm đóng message box
        cancelMessageBox(){
            this.warning = false;
        },

        cancelErrorBox(){
            this.error = false;
            if(this.field !=null){
                EventBus.$emit("focusAndSelectAll", this.field);
                this.field = null;
            }
        },

        // hàm hiện hộp cảnh báo
        showWarning(employee){
            this.employee = {...employee};
            this.warning = true;
        },

        // hàm xóa nhân viên
        deleteEmployee(){
            EventBus.$emit("deleteEmployee", this.employee);
            this.warning = false;
        },
        
        // hàm hiển thị message box thông báo lỗi
        showError(message, field){
            this.messageContent = message;
            this.error = true;
            this.field = field;
        }
    },

    mounted() {
        // sự kiện ẩn hiện loading
        EventBus.$on("onLoading", () => {
            this.loading = true;
        })

        // sự kiện tắt loading
        EventBus.$on("stopLoading", () => {
            this.loading = false;
        })

        // lắng nghe sự kiện hiện form cảnh báo
        EventBus.$on("showWarning", (employee) => {
            this.showWarning(employee);
        })

        // lắng nghe sự kiện hiện thông báo lỗi
        EventBus.$on("showError", (content ,field)=> {
           this.showError(content, field);
        })
    }
}
</script>

<style src="../css/loading.css"></style>
<style src="../css/googlefont.css"></style>