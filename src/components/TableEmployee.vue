<template>
  <div class="wrap-table" id="wrap_table">
    <div class="ms-table">
      <table>
        <caption></caption>
        <thead>
          <tr>
            <th id="" class="out-left-white bgc-white"></th>
            <th style="left: 16px" class="check-all" id="check-all">
              <input type="checkbox" name="" id="" />
            </th>
            <th style="min-width: 150px; width: 150px" id="">MÃ NHÂN VIÊN</th>
            <th style="min-width: 250px; width: 250px" id="">TÊN NHÂN VIÊN</th>
            <th style="min-width: 120px; width: 120px" id="">GIỚI TÍNH</th>
            <th style="min-width: 150px; width: 150px" id="">NGÀY SINH</th>
            <th style="min-width: 200px; width: 200px" id="">SỐ CMND</th>
            <th style="min-width: 250px; width: 250px" id="">CHỨC DANH</th>
            <th style="min-width: 250px; width: 250px" id="">TÊN ĐƠN VỊ</th>
            <th style="min-width: 150px; width: 150px" id="">SỐ TÀI KHOẢN</th>
            <th style="min-width: 250px; width: 250px" id="">TÊN NGÂN HÀNG</th>
            <th
              style="min-width: 250px; width: 250px; border-right: none"
              id=""
            >
              CHI NHÁNH TK NGÂN HÀNG
            </th>
            <th
              style="width: 120px; min-width: 120px; right: 30px"
              class="wiget border-left"
              id=""
            >
              <div>CHỨC NĂNG</div>
            </th>
            <th style="right: 0px" id="" class="out-right bgc-white"></th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(employee, index) in employeeList"
            :key="index"
            v-on:click="active(index)"
            v-on:dblclick="edit(index)"
          >
            <td id="" class="out-left-white bgc-white"></td>
            <td
              v-bind:class="{ selected: index == selectedRow }"
              class="col-check"
            >
              <input type="checkbox" name="" id="" />
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.employeeCode }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.employeeName }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ convertGender(employee.gender) }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              <div class="center">{{ convertDate(employee.dateOfBirth) }}</div>
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.identityNumber }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.employeePosition }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.departmentName }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.bankAccountNumber }}
            </td>
            <td v-bind:class="{ selected: index == selectedRow }">
              {{ employee.bankName }}
            </td>
            <td
              style="border-right: none"
              v-bind:class="{ selected: index == selectedRow }"
            >
              {{ employee.bankBranchName }}
            </td>
            <td
              style="right: 30px"
              v-bind:class="{ selected: index == selectedRow }"
              class="sticky border-left"
            >
              <div class="col-edit">
                <span v-on:click="edit(index)">Sửa</span>
                <div class="div-more-icon">
                  <img
                    src="../Resource/img/more.svg"
                    alt=""
                    @click="showMoreOption($event, index)"
                  />
                </div>
              </div>
            </td>
            <td style="right: 0px" class="out-right bgc-white"></td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="wrap-footer">
      <div class="footer-content">
        <div class="count-record">
          <span
            >Tổng số: <b>{{ countEmployee }}</b> bản ghi</span
          >
        </div>
        <div class="right-footer">
          <div class="div-select-count">
            <div style="position: sticky; left: 60%; right: 40%">
              <div
                style="
                  max-width: 215px;
                  max-height: 32px;
                  display: flex;
                  top: -17px;
                  border: 1px solid #babec5;
                  margin: 0 16px;
                "
                data-app
              >
                <v-select
                  :items="optionCountPerPage"
                  v-model="countEmployeePerPage"
                  @change="changeCountPerPage"
                  solo
                ></v-select>
              </div>
            </div>
          </div>
          <div class="wrap-paging">
            <v-container class="max-width">
              <v-pagination
                v-model="selectedPage"
                :length="countPage"
                @input="getDataPage()"
                :total-visible="7"
              ></v-pagination>
            </v-container>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
const axios = require("axios");
import EventBus from "../main.js";
export default {
  props: {
    keySearch: String,
  },

  data() {
    return {
      employeeList: {}, // những nhân viên hiển thị lên màn hình
      countEmployee: 0, // số nhân viên lấy/tìm kiếm được
      selectedRow: 0, // hàng đang chọn
      countEmployeePerPage: 10, // số lượng bản ghi / trang
      countPage: 1, // số trang
      selectedPage: 1, // trang đang chọn
      employeeNameWarning: null, // tên khách hàng định xóa
      employeeCodeWarning: null, // mã khách hàng định xóa
      optionCountPerPage: [
        { value: 10, text: "10 bản ghi trên 1 trang" },
        { value: 20, text: "20 bản ghi trên 1 trang" },
        { value: 30, text: "30 bản ghi trên 1 trang" },
        { value: 50, text: "50 bản ghi trên 1 trang" },
        { value: 100, text: "100 bản ghi trên 1 trang" },
      ],
    };
  },

  methods: {
    async getTotalRecord(mode) {
      let me = this;
      EventBus.$emit("onLoading");
      let keySearch = this._props.keySearch.trim();
      if(mode == "refresh") keySearch="";
      await axios
        .request({
          method: "post",
          url: "http://localhost:8080/api/v1/Employees/GetTotalRecord",
          data: '"' + keySearch + '"',
          headers: { "Content-Type": "application/json" },
        })
        .then((res) => {
          if (res.data != "") me.countEmployee = res.data;
          else me.countEmployee = 0;
          me.selectedPage = 1;
          me.selectedRow = 0;
          me.getDataServer(mode);
        })
        .catch((err) => {
          console.log(err);
          EventBus.$emit("stopLoading");
          let message =
            "Lấy dữ liệu thất bại, vui lòng liên hệ Misa để được trợ giúp.";
          EventBus.$emit("showError", message);
        });
    },

    // chuyển đổi kiểu giới tính để hiển thị
    convertGender(gd) {
      switch (gd) {
        case 0:
          return "Nam";
        case 1:
          return "Nữ";
        case 2:
          return "Khác";
      }
    },

    // sự kiện khi ấn vào các hàng
    active(index) {
      this.selectedRow = index;
    },

    // sự kiện khi ấn sửa
    edit(index) {
      let employee = this.employeeList[index];
      employee.dateOfBirth = this.convertDate(employee.dateOfBirth);
      employee.identityDate = this.convertDate(employee.identityDate);
      EventBus.$emit("editEmployee", employee);
    },

    // hàm lấy dữ liệu từ server cho trang thứ page
    getDataPage() {
      this.selectedRow = 0;
      this.getDataServer();
    },

    // thay đổi số lượng nhân viên hiển thị trên 1 trang
    changeCountPerPage() {
      this.selectedRow = 0;
      this.selectedPage = 1;
      this.getDataServer();
    },

    // chuyển đổi ngày tháng năm từ server thành dd/mm/yyyy để hiển thị
    convertDate(dateSrc) {
      if (dateSrc == null) return;
      let date = new Date(dateSrc),
        year = date.getFullYear().toString(),
        month = (date.getMonth() + 1).toString().padStart(2, "0"),
        day = date.getDate().toString().padStart(2, "0");
      return `${day}/${month}/${year}`;
    },

    // hàm lấy dữ liệu từ server
    async getDataServer(mode) {
      EventBus.$emit("onLoading");
      let me = this;
      let keySearch = this._props.keySearch.trim();
      if(mode == "refresh") keySearch="";
      await axios
        .request({
          method: "post",
          url:
            "http://localhost:8080/api/v1/Employees/GetPage?page=" +
            me.selectedPage +
            "&count=" +
            me.countEmployeePerPage,
          data: '"' + keySearch + '"',
          headers: { "Content-Type": "application/json" },
        })
        .then((res) => {
          me.employeeList = res.data;
          me.countPage = Math.ceil(me.countEmployee / me.countEmployeePerPage);
          EventBus.$emit("stopLoading");
        })
        .catch((err) => {
          console.log(err);
          EventBus.$emit("stopLoading");
          let message =
            "Lấy dữ liệu thất bại, vui lòng liên hệ Misa để được trợ giúp.";
          EventBus.$emit("showError", message);
        });
    },

    showMoreOption(e, index) {
      let clientX = e.clientX,
        clientY = e.clientY,
        employee = this.employeeList[index];
      this.$emit("showMoreOption", clientX, clientY, employee);
    },

    arrowUp() {
      console.log("up");
      this.selectedRow = this.selectedRow + 1;
    },
  },

  created() {
    this.getTotalRecord();
  },

  mounted() {
    //hàm lắng nghe sự kiện khi các component khác gọi loadData
    EventBus.$on("loadDataServer", (mode) => {
      this.getTotalRecord(mode);
    });

    EventBus.$on("arrowUp", () => {
      if (this.selectedRow > 0) this.selectedRow--;
    });

    EventBus.$on("arrowDown", () => {
      this.selectedRow++;
    });
  },
};
</script>

<style src="../css/googlefont.css"></style>
<style src="../css/table.css"></style>
