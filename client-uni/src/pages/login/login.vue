
<template>
    <view class="container">
        <view class="header">
            <view class="title">管理系统</view>
            <view class="subtitle">简单 · 智能 · 高效 · 安全</view>
        </view>

        <view class="form">
            <view class="form-item">
                <uni-icons class="icon" type="person" size="20" color="#8a8a8a" />
                <input class="input" type="text" v-model="account" placeholder="请输入用户名" maxlength="11" />
            </view>

            <view class="form-item">
                <uni-icons class="icon" type="locked" size="20" color="#8a8a8a" />
                <input class="input" :type="showPassword ? 'text' : 'password'" v-model="password" placeholder="请输入密码" />
                <uni-icons class="password-icon" :type="showPassword ? 'eye-filled' : 'eye-slash-filled'" size="20"
                    color="#8a8a8a" @click="togglePassword" />
            </view>

            <!-- <view class="remember">
          <view class="checkbox" @click="toggleRemember">
            <uni-icons 
              :type="isRemember ? 'checkbox-filled' : 'square'"
              size="18"
              :color="isRemember ? '#4080ff' : '#8a8a8a'"
            />
            <text class="remember-text">记住密码</text>
          </view>
        </view> -->
            <view class="remember">
                <checkbox-group @change="toggleRemember">
                    <checkbox :checked="isRemember" value="remember" color="#1A59F7" style="transform:scale(0.8)" />
                    <text class="remember-text">记住密码</text>
                </checkbox-group>
            </view>
            <button class="login-btn" @click="handleLogin">登 陆</button>
        </view>
    </view>
</template>
  
<script lang="ts" setup>
import { ref } from 'vue';
import { useUserStore } from "@/store/user";
const userStore = useUserStore();

const account = ref('admin');
const password = ref('123456');
const showPassword = ref(false);
const isRemember = ref(false);

const togglePassword = () => {
    showPassword.value = !showPassword.value;
};

const toggleRemember = () => {
    isRemember.value = !isRemember.value;
};

const handleLogin = async () => {
    if (!account.value) {
        uni.showToast({
            title: '请输入手机号',
            icon: 'none'
        });
        return;
    }
    if (!password.value) {
        uni.showToast({
            title: '请输入密码',
            icon: 'none'
        });
        return;
    }
    // 处理登录逻辑
    await userStore.login({ account: account.value, password: password.value });
};
</script>
  
<style>
page {
    height: 100%;
}

.container {
    display: flex;
    flex-direction: column;
    align-items: center;
    min-height: 100%;
    padding: 0 40rpx;
    background-color: #ffffff;
}

.header {
    margin-top: 160rpx;
    text-align: center;
}

.title {
    font-size: 40px;
    font-weight: 600;
    color: #4080ff;
    margin-bottom: 20rpx;
}

.subtitle {
    font-size: 14px;
    color: #8a8a8a;
    letter-spacing: 2px;
}

.form {
    width: 100%;
    margin-top: 100rpx;
}

.form-item {
    position: relative;
    display: flex;
    align-items: center;
    height: 100rpx;
    margin-bottom: 30rpx;
    padding: 0 30rpx;
    background-color: #f5f7fa;
    border-radius: 50rpx;
}

.icon {
    width: 20px;
    height: 20px;
    margin-right: 20rpx;
}

.input {
    flex: 1;
    height: 100rpx;
    font-size: 14px;
    color: #333333;
}

.password-icon {
    width: 20px;
    height: 20px;
}

.remember {
    display: flex;
    align-items: center;
    margin: 20rpx 0 40rpx;
}

.checkbox {
    display: flex;
    align-items: center;
    cursor: pointer;
}

.remember-text {
    font-size: 14px;
    color: #8a8a8a;
    margin-left: 10rpx;
}

.login-btn {
    width: 100%;
    height: 100rpx;
    line-height: 100rpx;
    text-align: center;
    background-color: #4080ff;
    color: #ffffff;
    font-size: 16px;
    border-radius: 50rpx;
}

.login-btn:active {
    opacity: 0.8;
}
</style>
  
  