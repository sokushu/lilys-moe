<template>
    <div id="app">
        <mu-container>
            <mu-card style="width: 100%; max-width: 375px; margin: 0 auto;">
                <mu-card-header title="Myron Avatar" sub-title="sub title">
                    <mu-avatar slot="avatar">
                        <img src="1300354_p0.jpg">
                    </mu-avatar>
                </mu-card-header>
                <mu-card-media title="Image Title" sub-title="Image Sub Title">
                    <img src="11300_p0.jpg">
                </mu-card-media>
                <mu-card-title title="Content Title" sub-title="Content Title"></mu-card-title>
                <mu-card-text>
                    public const string FilePath = @"E:\TEST\";
                    public const string FilePath_VersionLog = @"E:\TEST\TestLog";
                    public const string DateBasePath = "";
                </mu-card-text>
                <mu-card-actions>
                    <mu-button flat>Action 1</mu-button>
                    <mu-button flat>Action 2</mu-button>
                </mu-card-actions>
            </mu-card>
        </mu-container>

    </div>
</template>
<script>

</script>