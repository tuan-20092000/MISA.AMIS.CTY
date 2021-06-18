<template>
  <div>
    <Main/>
    <Loading />
  </div>
</template>

<script>
import Main from "../components/Main.vue";
import EmployeeDetail from "../components/EmployeeDetail.vue";
import Loading from "../components/Loading.vue";
export default {
  name: "Home",

  components: {
    Main, EmployeeDetail, Loading,
  },
};
</script>


<style src="../css/app.css"></style>
<style src="../css/googlefont.css"></style>

