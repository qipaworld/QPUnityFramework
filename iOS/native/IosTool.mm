#import "IosTool.h"
#import <Social/Social.h>
#import <StoreKit/StoreKit.h>
#import "NativeSocialShare.h"

NSString * const STR_SPLITTER = @"|";
NSString * const STR_EOF = @"endofline";
NSString * const STR_ARRAY_SPLITTER = @"%%%";

@implementation IosTool

+(NSString *) charToNSString:(char *)value {
    if (value != NULL) {
        return [NSString stringWithUTF8String: value];
    } else {
        return [NSString stringWithUTF8String: ""];
    }
}

+(const char *)NSIntToChar:(NSInteger)value {
    NSString *tmp = [NSString stringWithFormat:@"%ld", (long)value];
    return [tmp UTF8String];
}

+ (const char *) NSStringToChar:(NSString *)value {
    return [value UTF8String];
}

+ (NSArray *)charToNSArray:(char *)value {
    NSString* strValue = [self charToNSString:value];
    
    NSArray *array;
    if([strValue length] == 0) {
        array = [[NSArray alloc] init];
    } else {
        array = [strValue componentsSeparatedByString:STR_ARRAY_SPLITTER];
    }
    
    return array;
}

+ (const char *) NSStringsArrayToChar:(NSArray *) array {
    return [self NSStringToChar:[self serializeNSStringsArray:array]];
}

+ (NSString *) serializeNSStringsArray:(NSArray *) array {
    
    NSMutableString * data = [[NSMutableString alloc] init];
    
    
    for(NSString* str in array) {
        [data appendString:str];
        [data appendString: STR_ARRAY_SPLITTER];
    }
    
    [data appendString: STR_EOF];
    
    NSString *str = [data copy];
#if UNITY_VERSION < 500
    [str autorelease];
#endif
    
    return str;
}


+ (NSMutableString *)serializeErrorToNSString:(NSError *)error {
    NSString* description = @"";
    if(error.description != nil) {
        description = error.description;
    }
    
    return  [self serializeErrorWithDataToNSString:description code: (int) error.code];
}

+ (NSMutableString *)serializeErrorWithDataToNSString:(NSString *)description code:(int)code {
    NSMutableString * data = [[NSMutableString alloc] init];
    
    [data appendFormat:@"%i", code];
    [data appendString: STR_SPLITTER];
    [data appendString: description];
    
    return  data;
}


+ (const char *) serializeErrorWithData:(NSString *)description code: (int) code {
    NSString *str = [self serializeErrorWithDataToNSString:description code:code];
    return [self NSStringToChar:str];
}

+ (const char *) serializeError:(NSError *)error  {
    NSString *str = [self serializeErrorToNSString:error];
    return [self NSStringToChar:str];
}

@end

extern "C" {
    void IOS_PopScoreBox() {
        if([SKStoreReviewController respondsToSelector:@selector(requestReview)]){
            [SKStoreReviewController requestReview];
        }
    }
    
    void IOS_NativeShare(char* text, char* encodedMedia) {
        NSString *status = [IosTool charToNSString:text];
        NSString *media = [IosTool charToNSString:encodedMedia];
        
        [NativeSocialShare nativeShare:status media:media];
    }
}
