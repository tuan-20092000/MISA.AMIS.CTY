<template>
  <div class="main" @keyup.up.exact="arrowUp()" @keyup.down.exact="arrowDown()" @click="clickMain($event)">
    <div class="main-title">
      <div class="main-title-content">
        <div class="title">Nhân viên</div>
        <div class="main-title-btn">
          <button class="btn-more">
            <span>Tiện ích</span><img src="../Resource/img/drop_down.svg" alt="" />
          </button>
          <button v-on:click="addEmployee()" class="btn-add">Thêm</button>
          <button class="btn-excel">
            <img src="../Resource/img/drop_down.svg" alt="" />
          </button>
        </div>
      </div>
      </div>
    <div class="layout">
        <div class="grid">
          <div class="excution">
            <span>Thực hiện hàng loạt</span>
            <img src="../Resource/img/drop_down.svg" alt="" />
          </div>
        <div class="right-control">
          <div class="search-name-id">
            <input
              id="search-name-id"
              v-model="keySearch"
              ref="input"
              type="text"
              v-on:keyup.enter="searchKeyWord()"
              placeholder="Tìm theo mã, tên nhân viên"
              autocomplete="off"
            />
            <img src="../Resource/img/find.svg" alt="search" />
          </div>
          <div class="three-btn">
            <img v-on:click="refresh()" src="../Resource/img/refresh.svg" alt="" />
            <img src="../Resource/img/excel.svg" alt="" />
            <img src="../Resource/img/setting.svg" alt="" />
          </div>
        </div>
      </div>
      <div class="" id="ms-component-table">
        <TableEmployee 
          v-on:showMoreOption="showMoreOption"
          :keySearch="keySearch"/>
      </div>
    </div>
    <div class="div-left-30"></div>
    <div v-if="showOption" :style="{left:xClick+'px',top:yClick+'px'}" class="tool-tip" id="popupOption">
        <div class="option" @click="cloneEmployee()">Nhân bản</div>
        <div class="option" @click="showWarning()">Xóa</div>
    </div>
    
  </div>
</template>

<script>
import TableEmployee from "./TableEmployee.vue";
import EventBus from "./../main.js";
export default {
  data(){
    return{
      showOption: false, // ẩn hiện option xóa, nhân bản
      xClick: 0,
      yClick: 0,
      employee: null,
      keySearch:"",
    }
  },

  components: {
    TableEmployee,
  },

  methods: {
    // hiển thị form thêm mới nhân viên
    addEmployee() {
      EventBus.$emit("addEmployee");
    },

    // sự kiện ấn refresh
    refresh() {
      this.keySearch = "";
      let mode = "refresh";
      EventBus.$emit("loadDataServer", mode);
      
    },

    arrowUp() {
      EventBus.$emit("arrowUp");
    },

    arrowDown() {
      EventBus.$emit("arrowDown");
    },

    showMoreOption(xClick, yClick, employee){
      this.xClick = xClick - 280;
      this.yClick = yClick - 40;
      this.employee = employee;
      this.showOption = true;
    },

    // show form cảnh báo trước khi xóa
    showWarning() {
      EventBus.$emit("showWarning", this.employee);
      this.showOption = false;
    },

    clickMain(e){
      let component = document.getElementById("ms-component-table");
      if (component != null && this.showOption) {
        if (!component.contains(e.target)) {
          this.showOption = false;
        }
      }
    },

    // chuyển đổi ngày tháng năm từ server thành dd/mm/yyyy để hiển thị
    convertDate(dateSrc) {
      if(dateSrc == null) return;
      let date = new Date(dateSrc),
        year = date.getFullYear().toString(),
        month = (date.getMonth() + 1).toString().padStart(2, "0"),
        day = date.getDate().toString().padStart(2, "0");
      return `${day}/${month}/${year}`;
    },

    searchKeyWord(){
      EventBus.$emit("loadDataServer");
    },

    cloneEmployee(){
      let employee = {...this.employee};
      employee.dateOfBirth = this.convertDate(employee.dateOfBirth);
      employee.identityDate = this.convertDate(employee.identityDate);
      EventBus.$emit("addEmployee", employee);
    }
  },
};
</script>

<style src="../css/main.css"></style>
